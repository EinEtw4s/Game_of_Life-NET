using System.Media;

namespace Game_of_Life;

partial class MainForm1
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
    private void InitializeComponent()
    {
            this.pictureBox = new Game_of_Life.PictureBoxWithInterpolationMode();
            this.debugLabel = new System.Windows.Forms.Label();
            this.scalingSlider = new System.Windows.Forms.TrackBar();
            this.buttonRepopulate = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.scalingSlider)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox
            // 
            this.pictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Default;
            this.pictureBox.Location = new System.Drawing.Point(0, 0);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(800, 450);
            this.pictureBox.TabIndex = 0;
            this.pictureBox.TabStop = false;
            this.pictureBox.Click += new System.EventHandler(this.pictureBox_Click);
            this.pictureBox.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
            this.pictureBox.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseWheel);
            // 
            // debugLabel
            // 
            this.debugLabel.AutoSize = true;
            this.debugLabel.BackColor = System.Drawing.Color.Transparent;
            this.debugLabel.ForeColor = System.Drawing.Color.AliceBlue;
            this.debugLabel.Location = new System.Drawing.Point(12, 9);
            this.debugLabel.Name = "debugLabel";
            this.debugLabel.Size = new System.Drawing.Size(0, 15);
            this.debugLabel.TabIndex = 1;
            // 
            // scalingSlider
            // 
            this.scalingSlider.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.scalingSlider.Location = new System.Drawing.Point(12, 405);
            this.scalingSlider.Maximum = 500;
            this.scalingSlider.Minimum = 10;
            this.scalingSlider.Name = "scalingSlider";
            this.scalingSlider.Size = new System.Drawing.Size(178, 45);
            this.scalingSlider.TabIndex = 2;
            this.scalingSlider.Value = 10;
            this.scalingSlider.ValueChanged += new System.EventHandler(this.scalingSlider_Changed);
            // 
            // buttonRepopulate
            // 
            this.buttonRepopulate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonRepopulate.Location = new System.Drawing.Point(701, 12);
            this.buttonRepopulate.Name = "buttonRepopulate";
            this.buttonRepopulate.Size = new System.Drawing.Size(87, 23);
            this.buttonRepopulate.TabIndex = 3;
            this.buttonRepopulate.Text = "REPOPULATE";
            this.buttonRepopulate.UseVisualStyleBackColor = true;
            this.buttonRepopulate.Click += new System.EventHandler(this.buttonRepopulate_Click);
            // 
            // MainForm1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.buttonRepopulate);
            this.Controls.Add(this.scalingSlider);
            this.Controls.Add(this.debugLabel);
            this.Controls.Add(this.pictureBox);
            this.Name = "MainForm1";
            this.Text = "MainForm1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.scalingSlider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private PictureBoxWithInterpolationMode pictureBox;
    private Label debugLabel;
    private TrackBar scalingSlider;
    private Button buttonRepopulate;
}