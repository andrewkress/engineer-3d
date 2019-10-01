Imports EdmLib

Public Class SimpleTask

    Implements IEdmAddIn5

    Public Sub GetAddInInfo(ByRef poInfo As EdmLib.EdmAddInInfo, ByVal poVault As EdmLib.IEdmVault5, ByVal poCmdMgr As EdmLib.IEdmCmdMgr5) Implements EdmLib.IEdmAddIn5.GetAddInInfo
        poInfo.mbsAddInName = "Minimal VB.NET Task Add-In"
        poInfo.mbsCompany = "EPDM API Fundamentals Course"
        poInfo.mbsDescription = "Example demonstrating the minimum requirements for a task add-in."
        poInfo.mlAddInVersion = 1
        poInfo.mlRequiredVersionMajor = 10
        poInfo.mlRequiredVersionMinor = 0
        'MsgBox("loaded add-in")

        poCmdMgr.AddHook(EdmCmdType.EdmCmd_TaskRun)
        'poCmdMgr.AddHook(EdmCmdType.
        poCmdMgr.AddHook(EdmCmdType.EdmCmd_TaskSetup)
    End Sub

    Public Sub OnCmd(ByRef poCmd As EdmLib.EdmCmd, ByRef ppoData As System.Array) Implements EdmLib.IEdmAddIn5.OnCmd
        'MsgBox("OnCmd fired, action = " + poCmd.ToString())
        Try
            Select Case poCmd.meCmdType
                Case EdmCmdType.EdmCmd_TaskRun
                    OnTaskRun(poCmd, ppoData)
                Case EdmCmdType.EdmCmd_TaskSetup
                    OnTaskSetup(poCmd, ppoData)
            End Select
        Catch ex As Runtime.InteropServices.COMException
            MsgBox("HRESULT = 0x" + _
            ex.ErrorCode.ToString("X") + vbCrLf + _
            ex.Message)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub OnTaskRun(ByRef poCmd As EdmCmd, ByRef ppoData As Array)
        Dim inst As IEdmTaskInstance
        inst = poCmd.mpoExtra
        Try
            inst.SetStatus(EdmTaskStatus.EdmTaskStat_Running)
            inst.SetProgressRange(10, 1, "Test is running.")
            System.Threading.Thread.Sleep(5000)
            inst.SetProgressPos(5, "Test is running.")
            System.Threading.Thread.Sleep(5000)
            inst.SetProgressPos(10, "Test is done.")
            System.Threading.Thread.Sleep(5000)

            inst.SetStatus(EdmTaskStatus.EdmTaskStat_DoneOK)
        Catch ex As Runtime.InteropServices.COMException
            inst.SetStatus(EdmTaskStatus.EdmTaskStat_DoneFailed, ex.ErrorCode, "The test task failed!")
        End Try
    End Sub

    Private Sub OnTaskSetup(ByRef poCmd As EdmCmd, ByRef ppoData As Array)
        'MsgBox("on setup - loaded add-in")
        Dim props As IEdmTaskProperties
        props = poCmd.mpoExtra
        props.TaskFlags = EdmTaskFlag.EdmTask_SupportsInitExec + EdmTaskFlag.EdmTask_SupportsScheduling
        Dim cmds(0) As EdmTaskMenuCmd
        cmds(0).mbsMenuString = "Run the test task"
        cmds(0).mbsStatusBarHelp = "This command runs the task add-in."
        cmds(0).mlCmdID = 1
        cmds(0).mlEdmMenuFlags = EdmMenuFlags.EdmMenu_Nothing
        props.SetMenuCmds(cmds)
    End Sub

End Class
