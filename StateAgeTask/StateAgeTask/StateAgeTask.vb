' ==============================================================
'
' Copyright 2009 - 2012 Dassault Systèmes SolidWorks Corporation
'
' ==============================================================

Imports EdmLib

Public Class StateAgeTask
  Implements IEdmAddIn5

  Dim SetupPageObj As SetupPage

  Public Sub GetAddInInfo( _
        ByRef poInfo As EdmLib.EdmAddInInfo, _
        ByVal poVault As EdmLib.IEdmVault5, _
        ByVal poCmdMgr As EdmLib.IEdmCmdMgr5) _
        Implements EdmLib.IEdmAddIn5.GetAddInInfo
        Try
            PauseToAttachProcess("GetAddInInfo")
            poInfo.mbsAddInName = "VB.NET State Age Add-IN"
            poInfo.mlRequiredVersionMajor = 10
            poInfo.mlRequiredVersionMinor = 0
            poInfo.mbsCompany = "EPDM API Fundamentals Course"
            poInfo.mbsDescription = "Example demonstrating a scheduled task that checkes the length of time files have been in their current state."
            poInfo.mlAddInVersion = 1

            poCmdMgr.AddHook(EdmCmdType.EdmCmd_TaskSetup)
            poCmdMgr.AddHook(EdmCmdType.EdmCmd_TaskSetupButton)
            poCmdMgr.AddHook(EdmCmdType.EdmCmd_TaskRun)
            poCmdMgr.AddHook(EdmCmdType.EdmCmd_TaskDetails)
        Catch ex As Runtime.InteropServices.COMException
            MsgBox("HRESULT = 0x" + _
               ex.ErrorCode.ToString("X") + vbCrLf + _
               ex.Message)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
  End Sub

  Public Sub OnCmd(ByRef poCmd As EdmLib.EdmCmd, _
      ByRef ppoData As System.Array) _
      Implements EdmLib.IEdmAddIn5.OnCmd
    Try
            PauseToAttachProcess(poCmd.meCmdType.ToString())

            Select Case poCmd.meCmdType
                Case EdmCmdType.EdmCmd_TaskSetup
                    OnTaskSetup(poCmd, ppoData)
                Case EdmCmdType.EdmCmd_TaskSetupButton
                    OnTaskSetupButton(poCmd, ppoData)
                Case EdmCmdType.EdmCmd_TaskRun
                    OnTaskRun(poCmd, ppoData)
                Case EdmCmdType.EdmCmd_TaskDetails
                    OnTaskDetails(poCmd, ppoData)
            End Select
        Catch ex As Runtime.InteropServices.COMException
            MsgBox("HRESULT = 0x" + _
               ex.ErrorCode.ToString("X") + vbCrLf + _
               ex.Message)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

  End Sub

  Private Sub PauseToAttachProcess( _
      ByVal callbackType As String)
    Try
            'If the debugger isn't already attached to a
            'process, 
            'If Not Debugger.IsAttached() Then
            'launch the debug dialog
            'Debugger.Launch()
            'OR
            'use a MsgBox dialog to pause execution
            'and allow the user time to attach it
            '       MsgBox("Attach debugger to process """ + _
            '       Process.GetCurrentProcess.ProcessName() + _
            '    """ for callback """ + callbackType + _
            '     """ before clicking OK.")
            '       End If

        Catch ex As Exception
      MsgBox(ex.Message)
    End Try
  End Sub

    Private Sub OnTaskSetup(ByRef poCmd As EdmCmd, ByRef ppoData As Array)
        Try
            Dim props As IEdmTaskProperties = poCmd.mpoExtra
            If Not props Is Nothing Then
                props.TaskFlags = EdmTaskFlag.EdmTask_SupportsScheduling + EdmTaskFlag.EdmTask_SupportsDetails
                SetupPageObj = New SetupPage(poCmd.mpoVault, props)
                SetupPageObj.CreateControl()
                SetupPageObj.LoadData(poCmd)
                Dim pages(0) As EdmTaskSetupPage
                pages(0).mbsPageName = "Choose states to check"
                pages(0).mlPageHwnd = SetupPageObj.Handle.ToInt32
                pages(0).mpoPageImpl = SetupPageObj
                props.SetSetupPages(pages)
            End If
        Catch ex As Runtime.InteropServices.COMException
            MsgBox("HRESULT = 0x" + _
               ex.ErrorCode.ToString("X") + vbCrLf + _
               ex.Message)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub OnTaskSetupButton(ByRef poCmd As EdmCmd, ByRef ppoData As Array)
        Try
            If poCmd.mbsComment = "OK" And Not SetupPageObj Is Nothing Then
                SetupPageObj.StoreData()
            End If
            SetupPageObj = Nothing
        Catch ex As Runtime.InteropServices.COMException
            MsgBox("HRESULT = 0x" + _
               ex.ErrorCode.ToString("X") + vbCrLf + _
               ex.Message)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub OnTaskRun(ByRef poCmd As EdmCmd, ByRef ppoData As Array)
        Try
            Dim TaskInstance As IEdmTaskInstance
            TaskInstance = poCmd.mpoExtra
            If Not TaskInstance Is Nothing Then
                TaskInstance.SetStatus(EdmTaskStatus.EdmTaskStat_Running)
                TaskInstance.SetProgressRange(100, 0, "Task is running.")

                Dim NoDays As String
                NoDays = TaskInstance.GetValEx("NoDaysVar")
                Dim States As String = ""
                States = TaskInstance.GetValEx("SelectedStatesVar")

                Dim Items As List(Of EdmSelItem2) = New List(Of EdmSelItem2)
                DoSearch(poCmd.mpoVault, States, NoDays, Items)
                Dim NotificationArray(Items.Count - 1) As EdmSelItem2
                Items.copyto(NotificationArray)
                Dim ProgressMsg As String
                If (Items.Count > 0) Then
                    ProgressMsg = "Found " + Items.Count.ToString() + " files."
                Else
                    ProgressMsg = ("No files found.")
                End If

                TaskInstance.SetProgressPos(100, ProgressMsg)
                TaskInstance.SetStatus(EdmTaskStatus.EdmTaskStat_DoneOK, 0, "", NotificationArray, ProgressMsg)
            End If
        Catch ex As Runtime.InteropServices.COMException
            MsgBox("HRESULT = 0x" + _
               ex.ErrorCode.ToString("X") + vbCrLf + _
               ex.Message)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub OnTaskDetails(ByRef poCmd As EdmCmd, ByRef ppoData As Array)
        Try
            Dim TaskInstance As IEdmTaskInstance = poCmd.mpoExtra
            If Not TaskInstance Is Nothing Then
                SetupPageObj = New SetupPage(poCmd.mpoVault, TaskInstance)
                SetupPageObj.CreateControl()
                SetupPageObj.LoadData(poCmd)
                SetupPageObj.DisableControls()
                poCmd.mbsComment = "State Age Details"
                poCmd.mlParentWnd = SetupPageObj.Handle.ToInt32()
            End If
        Catch ex As Runtime.InteropServices.COMException
            MsgBox("HRESULT = 0x" + _
               ex.ErrorCode.ToString("X") + vbCrLf + _
               ex.Message)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub DoSearch(ByVal Vault As IEdmVault11, ByVal States As String, ByVal Days As String, ByVal Items As List(Of EdmSelItem2))
        Try
            Dim dt As DateTime = DateTime.Today
            Dim DaysInt As Integer = Convert.ToInt32(Days) - 1
            Dim ts As TimeSpan = New TimeSpan(DaysInt, 0, 0, 0)
            dt = dt.Subtract(ts)

            Dim StatesObj As Object = Split(States, vbCrLf)
            For Each State As String In StatesObj
                If Trim(State) = "" Then
                    Continue For
                End If

                Dim Search As IEdmSearch6 = Vault.CreateSearch()
                If Search Is Nothing Then Return

                Search.FindFiles = True
                Search.SetToken(EdmSearchToken.Edmstok_VersionsBefore, dt)
                Search.SetToken(EdmSearchToken.Edmstok_StateName, State)

                Dim SearchResult As IEdmSearchResult5 = Search.GetFirstResult()
                While Not SearchResult Is Nothing
                    Dim SelItem As EdmSelItem2 = New EdmSelItem2()
                    SelItem.mlID = SearchResult.ID
                    SelItem.mlParentID = SearchResult.ParentFolderID
                    SelItem.meType = SearchResult.ObjectType
                    SelItem.mlVersion = SearchResult.Version
                    Items.Add(SelItem)
                    SearchResult = Search.GetNextResult()
                End While
            Next State
        Catch ex As Runtime.InteropServices.COMException
            MsgBox("HRESULT = 0x" + _
               ex.ErrorCode.ToString("X") + vbCrLf + _
               ex.Message)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

End Class
