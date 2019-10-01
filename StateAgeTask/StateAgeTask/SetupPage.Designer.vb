<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SetupPage
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.StatesListBox = New System.Windows.Forms.ListBox
        Me.WorkflowsComboBox = New System.Windows.Forms.ComboBox
        Me.WorkflowLabel = New System.Windows.Forms.Label
        Me.StatesLabel = New System.Windows.Forms.Label
        Me.DaysLabel = New System.Windows.Forms.Label
        Me.DaysNumericUpDown = New System.Windows.Forms.NumericUpDown
        CType(Me.DaysNumericUpDown, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'StatesListBox
        '
        Me.StatesListBox.FormattingEnabled = True
        Me.StatesListBox.ItemHeight = 16
        Me.StatesListBox.Location = New System.Drawing.Point(25, 119)
        Me.StatesListBox.Name = "StatesListBox"
        Me.StatesListBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.StatesListBox.Size = New System.Drawing.Size(164, 132)
        Me.StatesListBox.TabIndex = 0
        '
        'WorkflowsComboBox
        '
        Me.WorkflowsComboBox.FormattingEnabled = True
        Me.WorkflowsComboBox.Location = New System.Drawing.Point(25, 43)
        Me.WorkflowsComboBox.Name = "WorkflowsComboBox"
        Me.WorkflowsComboBox.Size = New System.Drawing.Size(164, 24)
        Me.WorkflowsComboBox.TabIndex = 1
        '
        'WorkflowLabel
        '
        Me.WorkflowLabel.AutoSize = True
        Me.WorkflowLabel.Location = New System.Drawing.Point(27, 15)
        Me.WorkflowLabel.Name = "WorkflowLabel"
        Me.WorkflowLabel.Size = New System.Drawing.Size(117, 17)
        Me.WorkflowLabel.TabIndex = 2
        Me.WorkflowLabel.Text = "Choose Workflow"
        '
        'StatesLabel
        '
        Me.StatesLabel.AutoSize = True
        Me.StatesLabel.Location = New System.Drawing.Point(27, 89)
        Me.StatesLabel.Name = "StatesLabel"
        Me.StatesLabel.Size = New System.Drawing.Size(101, 17)
        Me.StatesLabel.TabIndex = 3
        Me.StatesLabel.Text = "Select State(s)"
        '
        'DaysLabel
        '
        Me.DaysLabel.AutoSize = True
        Me.DaysLabel.Location = New System.Drawing.Point(27, 275)
        Me.DaysLabel.Name = "DaysLabel"
        Me.DaysLabel.Size = New System.Drawing.Size(162, 17)
        Me.DaysLabel.TabIndex = 4
        Me.DaysLabel.Text = "Number of Days in State"
        '
        'DaysNumericUpDown
        '
        Me.DaysNumericUpDown.Location = New System.Drawing.Point(30, 304)
        Me.DaysNumericUpDown.Name = "DaysNumericUpDown"
        Me.DaysNumericUpDown.Size = New System.Drawing.Size(159, 22)
        Me.DaysNumericUpDown.TabIndex = 5
        '
        'SetupPage
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.DaysNumericUpDown)
        Me.Controls.Add(Me.DaysLabel)
        Me.Controls.Add(Me.StatesLabel)
        Me.Controls.Add(Me.WorkflowLabel)
        Me.Controls.Add(Me.WorkflowsComboBox)
        Me.Controls.Add(Me.StatesListBox)
        Me.Name = "SetupPage"
        Me.Size = New System.Drawing.Size(216, 342)
        CType(Me.DaysNumericUpDown, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents StatesListBox As System.Windows.Forms.ListBox
    Friend WithEvents WorkflowsComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents WorkflowLabel As System.Windows.Forms.Label
    Friend WithEvents StatesLabel As System.Windows.Forms.Label
    Friend WithEvents DaysLabel As System.Windows.Forms.Label
    Friend WithEvents DaysNumericUpDown As System.Windows.Forms.NumericUpDown

End Class
