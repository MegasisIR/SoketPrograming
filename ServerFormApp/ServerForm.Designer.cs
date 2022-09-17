namespace ServerFormApp;

partial class ServerForm
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }

        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private async void InitializeComponent()
    {
        this.btn = new System.Windows.Forms.Button();
        this.txtMessageLabel = new System.Windows.Forms.Label();
        this.btnSendToAll = new System.Windows.Forms.Button();
        this.txtBoxMessage = new System.Windows.Forms.TextBox();
        this.btnStopServer = new System.Windows.Forms.Button();
        this.txtShowMessages = new System.Windows.Forms.TextBox();
        this.SuspendLayout();
        // 
        // btn
        // 
        this.btn.Cursor = System.Windows.Forms.Cursors.Hand;
        this.btn.Location = new System.Drawing.Point(12, 341);
        this.btn.Name = "btn";
        this.btn.Size = new System.Drawing.Size(174, 30);
        this.btn.TabIndex = 0;
        this.btn.Text = "Accept Incoming Connetion";
        this.btn.Click += new System.EventHandler(this.BtnListenConnection);
        // 
        // txtMessageLabel
        // 
        this.txtMessageLabel.AutoSize = true;
        this.txtMessageLabel.Location = new System.Drawing.Point(445, 339);
        this.txtMessageLabel.Name = "txtMessageLabel";
        this.txtMessageLabel.Padding = new System.Windows.Forms.Padding(20, 5, 20, 5);
        this.txtMessageLabel.Size = new System.Drawing.Size(93, 25);
        this.txtMessageLabel.TabIndex = 1;
        this.txtMessageLabel.Text = "Message";
        this.txtMessageLabel.Click += new System.EventHandler(this.label1_Click);
        // 
        // btnSendToAll
        // 
        this.btnSendToAll.Cursor = System.Windows.Forms.Cursors.Hand;
        this.btnSendToAll.Location = new System.Drawing.Point(192, 341);
        this.btnSendToAll.Name = "btnSendToAll";
        this.btnSendToAll.Size = new System.Drawing.Size(174, 30);
        this.btnSendToAll.TabIndex = 2;
        this.btnSendToAll.Text = "Send To all clients";
        this.btnSendToAll.UseVisualStyleBackColor = true;
        this.btnSendToAll.Click += new System.EventHandler(this.BtnSendMessageToAllClients);
        // 
        // txtBoxMessage
        // 
        this.txtBoxMessage.Location = new System.Drawing.Point(544, 341);
        this.txtBoxMessage.Multiline = true;
        this.txtBoxMessage.Name = "txtBoxMessage";
        this.txtBoxMessage.PlaceholderText = "Write message for clients";
        this.txtBoxMessage.Size = new System.Drawing.Size(200, 30);
        this.txtBoxMessage.TabIndex = 3;
        // 
        // btnStopServer
        // 
        this.btnStopServer.Cursor = System.Windows.Forms.Cursors.Hand;
        this.btnStopServer.Location = new System.Drawing.Point(12, 394);
        this.btnStopServer.Name = "btnStopServer";
        this.btnStopServer.Size = new System.Drawing.Size(174, 30);
        this.btnStopServer.TabIndex = 4;
        this.btnStopServer.Text = "Stop Server";
        this.btnStopServer.UseVisualStyleBackColor = true;
        this.btnStopServer.Click += new System.EventHandler(this.BtnStopServer);
        // 
        // txtShowMessages
        // 
        this.txtShowMessages.Location = new System.Drawing.Point(12, 28);
        this.txtShowMessages.Multiline = true;
        this.txtShowMessages.Name = "txtShowMessages";
        this.txtShowMessages.Size = new System.Drawing.Size(759, 279);
        this.txtShowMessages.TabIndex = 5;
        // 
        // ServerForm
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(800, 450);
        this.Controls.Add(this.txtShowMessages);
        this.Controls.Add(this.btnStopServer);
        this.Controls.Add(this.txtBoxMessage);
        this.Controls.Add(this.btnSendToAll);
        this.Controls.Add(this.txtMessageLabel);
        this.Controls.Add(this.btn);
        this.Name = "ServerForm";
        this.Text = "Server Form";
        this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.BehaviorFormmClosing);
        this.ResumeLayout(false);
        this.PerformLayout();

    }

    #endregion

    private Button btn;
    private Label txtMessageLabel;
    private Button btnSendToAll;
    private TextBox txtBoxMessage;
    private Button btnStopServer;
    private TextBox txtShowMessages;
}