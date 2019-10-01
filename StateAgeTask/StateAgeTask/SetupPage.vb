' ==============================================================
'
' Copyright 2009 - 2012 Dassault Systèmes SolidWorks Corporation
'
' ==============================================================

Imports EdmLib

Public Class SetupPage

    Private mVault As IEdmVault7
    Private mTaskProps As IEdmTaskProperties
    Private mTaskInst As IEdmTaskInstance

    Public Sub New(ByVal Vault As IEdmVault7, _
        ByVal Props As IEdmTaskProperties)
        Try
            InitializeComponent()
            mVault = Vault
            mTaskProps = Props
            mTaskInst = Nothing
        Catch ex As Runtime.InteropServices.COMException
            MsgBox("HRESULT = 0x" + _
               ex.ErrorCode.ToString("X") + vbCrLf + _
               ex.Message)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Sub New(ByVal Vault As IEdmVault7, _
        ByVal Props As IEdmTaskInstance)
        Try
            InitializeComponent()
            mVault = Vault
            mTaskProps = Nothing
            mTaskInst = Props
        Catch ex As Runtime.InteropServices.COMException
            MsgBox("HRESULT = 0x" + _
               ex.ErrorCode.ToString("X") + vbCrLf + _
               ex.Message)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Sub LoadData(ByVal poCmd As EdmCmd)
        Try
            WorkflowsComboBox.Items.Clear()
            Dim WorkflowMgr As IEdmWorkflowMgr6
            WorkflowMgr = mVault.CreateUtility(EdmUtility.EdmUtil_WorkflowMgr)
            Dim WorkflowPos As IEdmPos5 = WorkflowMgr.GetFirstWorkflowPosition()
            While Not WorkflowPos.IsNull
                Dim Workflow As IEdmWorkflow6
                Workflow = WorkflowMgr.GetNextWorkflow(WorkflowPos)
                WorkflowsComboBox.Items.Add(Workflow.Name)
            End While

            Dim SelectedWorkflow As String = ""
            Dim NoDays As String = ""
            If Not mTaskProps Is Nothing Then
                SelectedWorkflow = mTaskProps.GetValEx("SelectedWorkflowVar")
                NoDays = mTaskProps.GetValEx("NoDaysVar")
            ElseIf Not mTaskInst Is Nothing Then
                SelectedWorkflow = mTaskInst.GetValEx("SelectedWorkflowVar")
                NoDays = mTaskInst.GetValEx("NoDaysVar")
            End If

            If SelectedWorkflow = "" Then
                WorkflowsComboBox.SelectedIndex = 0
            Else
                WorkflowsComboBox.Text = SelectedWorkflow
            End If

            If IsNumeric(NoDays) Then
                DaysNumericUpDown.Value = Convert.ToDecimal(NoDays)
            Else
                DaysNumericUpDown.Value = 0
            End If
        Catch ex As Runtime.InteropServices.COMException
            MsgBox("HRESULT = 0x" + _
               ex.ErrorCode.ToString("X") + vbCrLf + _
               ex.Message)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Sub StoreData()
        Try
            Dim StatesList As String = ""
            For Each SelectedStateIndex As Integer In StatesListBox.SelectedIndices
                StatesList += StatesListBox.Items(SelectedStateIndex) + vbCrLf
            Next
            mTaskProps.SetValEx("SelectedStatesVar", StatesList)
            mTaskProps.SetValEx("SelectedWorkflowVar", WorkflowsComboBox.Text)
            mTaskProps.SetValEx("NoDaysVar", DaysNumericUpDown.Value.ToString())
        Catch ex As Runtime.InteropServices.COMException
            MsgBox("HRESULT = 0x" + _
               ex.ErrorCode.ToString("X") + vbCrLf + _
               ex.Message)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub


    Private Sub WorkflowsComboBox_SelectedIndexChanged( _
        ByVal sender As System.Object, _
        ByVal e As System.EventArgs) _
        Handles WorkflowsComboBox.SelectedIndexChanged
        Try
            Dim WorkflowMgr As IEdmWorkflowMgr6
            WorkflowMgr = mVault.CreateUtility(EdmUtility.EdmUtil_WorkflowMgr)
            Dim WorkflowPos As IEdmPos5 = WorkflowMgr.GetFirstWorkflowPosition()
            Dim Workflow As IEdmWorkflow6 = Nothing
            Dim SelectedWorkflow As IEdmWorkflow6 = Nothing
            While Not WorkflowPos.IsNull
                Workflow = WorkflowMgr.GetNextWorkflow(WorkflowPos)
                If Workflow.Name = WorkflowsComboBox.Text Then
                    SelectedWorkflow = Workflow
                    Exit While
                End If
            End While

            StatesListBox.Items.Clear()
            If Not SelectedWorkflow Is Nothing Then
                Dim StatePos As IEdmPos5 = SelectedWorkflow.GetFirstStatePosition()
                While Not StatePos.IsNull
                    Dim State As IEdmState6
                    State = SelectedWorkflow.GetNextState(StatePos)
                    StatesListBox.Items.Add(State.Name)
                End While
            End If

            Dim SelectedStates As String = ""
            If Not mTaskProps Is Nothing Then
                SelectedStates = mTaskProps.GetValEx("SelectedStatesVar")
            ElseIf Not mTaskInst Is Nothing Then
                SelectedStates = mTaskInst.GetValEx("SelectedStatesVar")
            End If

            Dim States As Object = Split(SelectedStates, vbCrLf)
            For Each State As String In States
                If Not Trim(State) = "" Then
                    StatesListBox.SelectedItems.Add(State)
                End If
            Next
        Catch ex As Runtime.InteropServices.COMException
            MsgBox("HRESULT = 0x" + _
               ex.ErrorCode.ToString("X") + vbCrLf + _
               ex.Message)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Sub DisableControls()
        Try

        Catch ex As Runtime.InteropServices.COMException
            MsgBox("HRESULT = 0x" + _
               ex.ErrorCode.ToString("X") + vbCrLf + _
               ex.Message)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

End Class
