using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.IO;
using System.Security.Permissions;
using ZedGraph;

namespace VisualRegression
{
	/// <summary>
	/// Main class for the Visual Regression application. All the drawing and file 
    /// handling is done here, while most of the math is in the Statistics and 
    /// OLSRegression classes.
	/// </summary>
    public class FormVisualRegression : System.Windows.Forms.Form
    {
		/// <summary>
		/// Required designer variable.
		/// </summary>
        private System.ComponentModel.Container components = null;

        private TableLayoutPanel tableLayout;
        private ZedGraphControl graphb2TChart;
        private ZedGraphControl graphResiduals;
        public DataGridView dataGrid;
        private ZedGraphControl graphRegression;
        private Panel panel3;
        private Label label1;
        private NumericUpDown numSamples;
        private Label label4;
        private NumericUpDown numRound;
        private Label label3;
        private TextBox txtRegression;
        private Label label2;
        private NumericUpDown numSignificance;
        private DataGridViewTextBoxColumn dataResponses;
        private DataGridViewTextBoxColumn dataPredictors;
        private MenuStrip menuStrip;
        private ToolStripMenuItem menuFile;
        private ToolStripMenuItem menuOpen;
        private ToolStripMenuItem menuHelp;
        private ToolStripMenuItem menuExit;
        private ToolStripSeparator menuSeparator1;
        private ToolStripMenuItem menuNew;
        private ToolStripMenuItem menuAbout;
        private ToolStripMenuItem menuOpenTXT;
        public ToolStripMenuItem menuSave;
        private ToolStripSeparator menuSeparator2;
        private ToolStripMenuItem menuSaveAs;

        /// <value name="pointsData">List of the data points</value>
        private PointPairList pointsData;

        /// <value name="regression">Used to calculate the regression line and associated information</value>
        private OlsRegression regression;

        /// <value name="modified">Tracks if DataGridView has been modified</value>
        private bool modified;

        public bool Modified
        {
            set { modified = value; }
        }

        /// <value name="filename">Filename used to save to a file</value>
        private string fileName;

        public string FileName
        {
            set { fileName = value; }
        }

        /// <summary>
        /// Constructor that creates new instances of the data points lists and regression class.
        /// </summary>
		public FormVisualRegression()
		{
            // Create the list of data points
            pointsData = new PointPairList();

            // Initialize the regression analysis class
            regression = new OlsRegression();

			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.tableLayout = new System.Windows.Forms.TableLayoutPanel();
            this.graphb2TChart = new ZedGraph.ZedGraphControl();
            this.graphResiduals = new ZedGraph.ZedGraphControl();
            this.dataGrid = new System.Windows.Forms.DataGridView();
            this.dataResponses = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataPredictors = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.graphRegression = new ZedGraph.ZedGraphControl();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.numSignificance = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.numRound = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.txtRegression = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.numSamples = new System.Windows.Forms.NumericUpDown();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.menuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.menuNew = new System.Windows.Forms.ToolStripMenuItem();
            this.menuOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.menuOpenTXT = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.menuSave = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.menuExit = new System.Windows.Forms.ToolStripMenuItem();
            this.menuHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.menuAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).BeginInit();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSignificance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRound)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSamples)).BeginInit();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayout
            // 
            this.tableLayout.AutoSize = true;
            this.tableLayout.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayout.ColumnCount = 2;
            this.tableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayout.Controls.Add(this.graphb2TChart, 1, 2);
            this.tableLayout.Controls.Add(this.graphResiduals, 0, 2);
            this.tableLayout.Controls.Add(this.dataGrid, 0, 0);
            this.tableLayout.Controls.Add(this.graphRegression, 1, 0);
            this.tableLayout.Controls.Add(this.panel3, 0, 1);
            this.tableLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayout.Location = new System.Drawing.Point(0, 24);
            this.tableLayout.MinimumSize = new System.Drawing.Size(250, 250);
            this.tableLayout.Name = "tableLayout";
            this.tableLayout.RowCount = 3;
            this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayout.Size = new System.Drawing.Size(785, 619);
            this.tableLayout.TabIndex = 38;
            // 
            // graphb2TChart
            // 
            this.graphb2TChart.AutoSize = true;
            this.graphb2TChart.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.graphb2TChart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.graphb2TChart.IsAutoScrollRange = false;
            this.graphb2TChart.IsEnableHPan = true;
            this.graphb2TChart.IsEnableHZoom = true;
            this.graphb2TChart.IsEnableVPan = true;
            this.graphb2TChart.IsEnableVZoom = true;
            this.graphb2TChart.IsPrintFillPage = true;
            this.graphb2TChart.IsPrintKeepAspectRatio = true;
            this.graphb2TChart.IsScrollY2 = false;
            this.graphb2TChart.IsShowContextMenu = true;
            this.graphb2TChart.IsShowCopyMessage = true;
            this.graphb2TChart.IsShowCursorValues = false;
            this.graphb2TChart.IsShowHScrollBar = false;
            this.graphb2TChart.IsShowPointValues = false;
            this.graphb2TChart.IsShowVScrollBar = false;
            this.graphb2TChart.IsZoomOnMouseCenter = false;
            this.graphb2TChart.Location = new System.Drawing.Point(395, 335);
            this.graphb2TChart.Name = "graphb2TChart";
            this.graphb2TChart.PanButtons = System.Windows.Forms.MouseButtons.Left;
            this.graphb2TChart.PanButtons2 = System.Windows.Forms.MouseButtons.Middle;
            this.graphb2TChart.PanModifierKeys2 = System.Windows.Forms.Keys.None;
            this.graphb2TChart.PointDateFormat = "g";
            this.graphb2TChart.PointValueFormat = "G";
            this.graphb2TChart.ScrollMaxX = 0;
            this.graphb2TChart.ScrollMaxY = 0;
            this.graphb2TChart.ScrollMaxY2 = 0;
            this.graphb2TChart.ScrollMinX = 0;
            this.graphb2TChart.ScrollMinY = 0;
            this.graphb2TChart.ScrollMinY2 = 0;
            this.graphb2TChart.Size = new System.Drawing.Size(387, 281);
            this.graphb2TChart.TabIndex = 41;
            this.graphb2TChart.ZoomButtons = System.Windows.Forms.MouseButtons.Left;
            this.graphb2TChart.ZoomButtons2 = System.Windows.Forms.MouseButtons.None;
            this.graphb2TChart.ZoomModifierKeys = System.Windows.Forms.Keys.None;
            this.graphb2TChart.ZoomModifierKeys2 = System.Windows.Forms.Keys.None;
            this.graphb2TChart.ZoomStepFraction = 0.1;
            // 
            // graphResiduals
            // 
            this.graphResiduals.AutoSize = true;
            this.graphResiduals.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.graphResiduals.Dock = System.Windows.Forms.DockStyle.Fill;
            this.graphResiduals.IsAutoScrollRange = false;
            this.graphResiduals.IsEnableHPan = true;
            this.graphResiduals.IsEnableHZoom = true;
            this.graphResiduals.IsEnableVPan = true;
            this.graphResiduals.IsEnableVZoom = true;
            this.graphResiduals.IsPrintFillPage = true;
            this.graphResiduals.IsPrintKeepAspectRatio = true;
            this.graphResiduals.IsScrollY2 = false;
            this.graphResiduals.IsShowContextMenu = true;
            this.graphResiduals.IsShowCopyMessage = true;
            this.graphResiduals.IsShowCursorValues = false;
            this.graphResiduals.IsShowHScrollBar = false;
            this.graphResiduals.IsShowPointValues = false;
            this.graphResiduals.IsShowVScrollBar = false;
            this.graphResiduals.IsZoomOnMouseCenter = false;
            this.graphResiduals.Location = new System.Drawing.Point(3, 335);
            this.graphResiduals.Name = "graphResiduals";
            this.graphResiduals.PanButtons = System.Windows.Forms.MouseButtons.Left;
            this.graphResiduals.PanButtons2 = System.Windows.Forms.MouseButtons.Middle;
            this.graphResiduals.PanModifierKeys2 = System.Windows.Forms.Keys.None;
            this.graphResiduals.PointDateFormat = "g";
            this.graphResiduals.PointValueFormat = "G";
            this.graphResiduals.ScrollMaxX = 0;
            this.graphResiduals.ScrollMaxY = 0;
            this.graphResiduals.ScrollMaxY2 = 0;
            this.graphResiduals.ScrollMinX = 0;
            this.graphResiduals.ScrollMinY = 0;
            this.graphResiduals.ScrollMinY2 = 0;
            this.graphResiduals.Size = new System.Drawing.Size(386, 281);
            this.graphResiduals.TabIndex = 42;
            this.graphResiduals.ZoomButtons = System.Windows.Forms.MouseButtons.Left;
            this.graphResiduals.ZoomButtons2 = System.Windows.Forms.MouseButtons.None;
            this.graphResiduals.ZoomModifierKeys = System.Windows.Forms.Keys.None;
            this.graphResiduals.ZoomModifierKeys2 = System.Windows.Forms.Keys.None;
            this.graphResiduals.ZoomStepFraction = 0.1;
            // 
            // dataGrid
            // 
            this.dataGrid.AllowUserToResizeRows = false;
            this.dataGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGrid.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            this.dataGrid.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataResponses,
            this.dataPredictors});
            this.dataGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnKeystroke;
            this.dataGrid.Location = new System.Drawing.Point(3, 3);
            this.dataGrid.Name = "dataGrid";
            this.dataGrid.RowHeadersVisible = false;
            this.dataGrid.Size = new System.Drawing.Size(386, 281);
            this.dataGrid.TabIndex = 1;
            this.dataGrid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGrid_KeyDown);
            this.dataGrid.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataChanged);
            // 
            // dataResponses
            // 
            this.dataResponses.HeaderText = "Response";
            this.dataResponses.Name = "dataResponses";
            this.dataResponses.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataResponses.Width = 60;
            // 
            // dataPredictors
            // 
            this.dataPredictors.HeaderText = "Predictor";
            this.dataPredictors.Name = "dataPredictors";
            this.dataPredictors.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataPredictors.Width = 54;
            // 
            // graphRegression
            // 
            this.graphRegression.AutoSize = true;
            this.graphRegression.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.graphRegression.Dock = System.Windows.Forms.DockStyle.Fill;
            this.graphRegression.IsAutoScrollRange = false;
            this.graphRegression.IsEnableHPan = true;
            this.graphRegression.IsEnableHZoom = true;
            this.graphRegression.IsEnableVPan = true;
            this.graphRegression.IsEnableVZoom = true;
            this.graphRegression.IsPrintFillPage = true;
            this.graphRegression.IsPrintKeepAspectRatio = true;
            this.graphRegression.IsScrollY2 = false;
            this.graphRegression.IsShowContextMenu = true;
            this.graphRegression.IsShowCopyMessage = true;
            this.graphRegression.IsShowCursorValues = false;
            this.graphRegression.IsShowHScrollBar = false;
            this.graphRegression.IsShowPointValues = false;
            this.graphRegression.IsShowVScrollBar = false;
            this.graphRegression.IsZoomOnMouseCenter = false;
            this.graphRegression.Location = new System.Drawing.Point(395, 3);
            this.graphRegression.Name = "graphRegression";
            this.graphRegression.PanButtons = System.Windows.Forms.MouseButtons.Left;
            this.graphRegression.PanButtons2 = System.Windows.Forms.MouseButtons.Middle;
            this.graphRegression.PanModifierKeys2 = System.Windows.Forms.Keys.None;
            this.graphRegression.PointDateFormat = "g";
            this.graphRegression.PointValueFormat = "G";
            this.graphRegression.ScrollMaxX = 0;
            this.graphRegression.ScrollMaxY = 0;
            this.graphRegression.ScrollMaxY2 = 0;
            this.graphRegression.ScrollMinX = 0;
            this.graphRegression.ScrollMinY = 0;
            this.graphRegression.ScrollMinY2 = 0;
            this.graphRegression.Size = new System.Drawing.Size(387, 281);
            this.graphRegression.TabIndex = 44;
            this.graphRegression.ZoomButtons = System.Windows.Forms.MouseButtons.Left;
            this.graphRegression.ZoomButtons2 = System.Windows.Forms.MouseButtons.None;
            this.graphRegression.ZoomModifierKeys = System.Windows.Forms.Keys.None;
            this.graphRegression.ZoomModifierKeys2 = System.Windows.Forms.Keys.None;
            this.graphRegression.ZoomStepFraction = 0.1;
            // 
            // panel3
            // 
            this.tableLayout.SetColumnSpan(this.panel3, 2);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.numSignificance);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Controls.Add(this.numRound);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.txtRegression);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Controls.Add(this.numSamples);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(3, 290);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(779, 39);
            this.panel3.TabIndex = 46;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(592, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(109, 13);
            this.label2.TabIndex = 45;
            this.label2.Text = "Level of Significance:";
            // 
            // numSignificance
            // 
            this.numSignificance.DecimalPlaces = 3;
            this.numSignificance.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numSignificance.Location = new System.Drawing.Point(706, 10);
            this.numSignificance.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            196608});
            this.numSignificance.Minimum = new decimal(new int[] {
            501,
            0,
            0,
            196608});
            this.numSignificance.Name = "numSignificance";
            this.numSignificance.Size = new System.Drawing.Size(65, 20);
            this.numSignificance.TabIndex = 5;
            this.numSignificance.Value = new decimal(new int[] {
            950,
            0,
            0,
            196608});
            this.numSignificance.ValueChanged += new System.EventHandler(this.SamplesChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(447, 12);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(83, 13);
            this.label4.TabIndex = 43;
            this.label4.Text = "Decimal Places:";
            // 
            // numRound
            // 
            this.numRound.Location = new System.Drawing.Point(536, 9);
            this.numRound.Maximum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.numRound.Name = "numRound";
            this.numRound.Size = new System.Drawing.Size(50, 20);
            this.numRound.TabIndex = 4;
            this.numRound.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.numRound.ValueChanged += new System.EventHandler(this.SamplesChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(147, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(107, 13);
            this.label3.TabIndex = 41;
            this.label3.Text = "Regression Function:";
            // 
            // txtRegression
            // 
            this.txtRegression.BackColor = System.Drawing.SystemColors.Window;
            this.txtRegression.Location = new System.Drawing.Point(260, 9);
            this.txtRegression.Name = "txtRegression";
            this.txtRegression.ReadOnly = true;
            this.txtRegression.Size = new System.Drawing.Size(181, 20);
            this.txtRegression.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 13);
            this.label1.TabIndex = 31;
            this.label1.Text = "Data Row Limit:";
            // 
            // numSamples
            // 
            this.numSamples.Location = new System.Drawing.Point(91, 10);
            this.numSamples.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numSamples.Name = "numSamples";
            this.numSamples.Size = new System.Drawing.Size(50, 20);
            this.numSamples.TabIndex = 2;
            this.numSamples.ValueChanged += new System.EventHandler(this.SamplesChanged);
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuFile,
            this.menuHelp});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(785, 24);
            this.menuStrip.TabIndex = 39;
            this.menuStrip.Text = "menuStrip1";
            // 
            // menuFile
            // 
            this.menuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuNew,
            this.menuOpen,
            this.menuSeparator1,
            this.menuSave,
            this.menuSaveAs,
            this.menuSeparator2,
            this.menuExit});
            this.menuFile.Name = "menuFile";
            this.menuFile.Size = new System.Drawing.Size(35, 20);
            this.menuFile.Text = "File";
            // 
            // menuNew
            // 
            this.menuNew.Name = "menuNew";
            this.menuNew.Size = new System.Drawing.Size(136, 22);
            this.menuNew.Text = "New";
            this.menuNew.Click += new System.EventHandler(this.menuNew_Click);
            // 
            // menuOpen
            // 
            this.menuOpen.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuOpenTXT});
            this.menuOpen.Name = "menuOpen";
            this.menuOpen.Size = new System.Drawing.Size(136, 22);
            this.menuOpen.Text = "Open";
            // 
            // menuOpenTXT
            // 
            this.menuOpenTXT.Name = "menuOpenTXT";
            this.menuOpenTXT.Size = new System.Drawing.Size(149, 22);
            this.menuOpenTXT.Text = "Text/CSV File";
            this.menuOpenTXT.Click += new System.EventHandler(this.menuOpenTXT_Click);
            // 
            // menuSeparator1
            // 
            this.menuSeparator1.Name = "menuSeparator1";
            this.menuSeparator1.Size = new System.Drawing.Size(133, 6);
            // 
            // menuSave
            // 
            this.menuSave.Name = "menuSave";
            this.menuSave.Size = new System.Drawing.Size(136, 22);
            this.menuSave.Text = "Save";
            this.menuSave.Click += new System.EventHandler(this.menuSave_Click);
            // 
            // menuSaveAs
            // 
            this.menuSaveAs.Name = "menuSaveAs";
            this.menuSaveAs.Size = new System.Drawing.Size(136, 22);
            this.menuSaveAs.Text = "Save As...";
            this.menuSaveAs.Click += new System.EventHandler(this.menuSaveAs_Click);
            // 
            // menuSeparator2
            // 
            this.menuSeparator2.Name = "menuSeparator2";
            this.menuSeparator2.Size = new System.Drawing.Size(133, 6);
            // 
            // menuExit
            // 
            this.menuExit.Name = "menuExit";
            this.menuExit.Size = new System.Drawing.Size(136, 22);
            this.menuExit.Text = "Exit";
            this.menuExit.Click += new System.EventHandler(this.menuExit_Click);
            // 
            // menuHelp
            // 
            this.menuHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuAbout});
            this.menuHelp.Name = "menuHelp";
            this.menuHelp.Size = new System.Drawing.Size(40, 20);
            this.menuHelp.Text = "Help";
            // 
            // menuAbout
            // 
            this.menuAbout.Name = "menuAbout";
            this.menuAbout.Size = new System.Drawing.Size(200, 22);
            this.menuAbout.Text = "About Visual Regression";
            this.menuAbout.Click += new System.EventHandler(this.menuAbout_Click);
            // 
            // FormVisualRegression
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(785, 643);
            this.Controls.Add(this.tableLayout);
            this.Controls.Add(this.menuStrip);
            this.MainMenuStrip = this.menuStrip;
            this.MinimumSize = new System.Drawing.Size(400, 400);
            this.Name = "FormVisualRegression";
            this.Text = "Visual Regression";
            this.Load += new System.EventHandler(this.FormVisualRegression_Load);
            this.tableLayout.ResumeLayout(false);
            this.tableLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSignificance)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRound)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSamples)).EndInit();
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new FormVisualRegression());
		}

        /// <summary>
        /// Test if an object, when converted to a string can be converted to a numeric value.
        /// </summary>
        /// <returns>
        /// true if object.ToString() is a numeric value or period, false if not
        /// </returns>
        /// <param name="objectTest">object that will be converted to a string for numeric parsing</param>
        public static bool IsNumeric(object objectTest)
        {
            double newVal;

            if (null == objectTest) { return false; }

            // Special case to handle decimal points
            if (objectTest.ToString() == ".") { return true; }

            try
            {
                return double.TryParse(objectTest.ToString(), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out newVal);
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Test if an char is a delimiter character, such as a space, tab, colon, semi-colon, comma, or pipe
        /// </summary>
        /// <returns>
        /// true if IsSeparator, IsWhiteSpace, or chr == ':' ',' '|'
        /// </returns>
        /// <param name="chr">char to test as a delimiter</param>
        public static bool IsDelimiter(char chr)
        {
            if (char.IsSeparator(chr) || char.IsWhiteSpace(chr) || ':' == chr || ',' == chr || '|' == chr)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Refreshes the class member pointsData with the values from the DataGridView
        /// </summary>
        private void LoadRegressionData()
        {
            pointsData.Clear();

            if (!IsNumeric(numSamples.Value)) { return; }

            for (int i = 0; i < dataGrid.Rows.Count; i++)
            {
                // Check if the sample limit has been hit
                if (numSamples.Value > 0 && numSamples.Value < i + 1) { break; }

                if (IsNumeric(dataGrid.Rows[i].Cells[0].Value) &&
                    IsNumeric(dataGrid.Rows[i].Cells[1].Value))
                {
                    pointsData.Add(System.Convert.ToDouble(dataGrid.Rows[i].Cells[1].Value),
                                   System.Convert.ToDouble(dataGrid.Rows[i].Cells[0].Value));
                }
            }
        }

        /// <summary>
        /// Plot the points from pointsData on the regression graph
        /// </summary>
        private void DrawRegressionData(GraphPane graph)
        {
            LineItem lineRegression = graph.AddCurve("Data Points", pointsData, Color.Black, SymbolType.Circle);
            lineRegression.Line.Color = Color.Transparent;
        }

        /// <summary>
        /// Run a regression on the pointsData values and draw a regression line and confidence band
        /// </summary>
        /// <param name="graph">GraphPane to draw the regression line and confidence bands on</param>
        private void DrawRegressionLine(GraphPane graph)
        {
            regression.Clear();

            // We need at least three x,y pairs to get one degree of freedom or more
            if (pointsData.Count < 3) { return; }

            double[] responses = new double[pointsData.Count];
            double[] predictors = new double[pointsData.Count];

            // Convert the PointPairList in to two double[] arrays
            for (int i = 0; i < pointsData.Count; i++)
            {
                responses[i] = pointsData[i].Y;
                predictors[i] = pointsData[i].X;
            }

            // Do the regression math
            regression.Calculate(responses, predictors);

            // Display the estimated equation
            string sign = (regression.b2 >= 0) ? " + " : " - ";

            txtRegression.Text = "Y = " + Math.Round(regression.b1, Convert.ToInt32(numRound.Value)) + sign +
                                 Math.Abs(Math.Round(regression.b2, Convert.ToInt32(numRound.Value))) + "X";

            // Sort the x array to find the least and greatest values
            Array.Sort(predictors);

            // Draw the regression line
            ///////////////////////////////
            PointPairList pointsRegressionLine = new PointPairList();
            pointsRegressionLine.Add(predictors[0], regression.b1 + regression.b2 * predictors[0]);
            pointsRegressionLine.Add(predictors[predictors.Length - 1], regression.b1 + regression.b2 * predictors[predictors.Length - 1]);
            graph.AddCurve("Regression Line", pointsRegressionLine, Color.Red, SymbolType.None);
            ///////////////////////////////

            // Draw the confidence band
            ///////////////////////////////
            double tval;
            //!!! 50 Is an arbitrary number, we can reduce this for a performance boost
            double MAX = 50.0D;
            PointPairList pointsUpperConfidence = new PointPairList();
            PointPairList pointsLowerConfidence = new PointPairList();

            for (double i = 0.0D; i < MAX; i += 1.0D)
            {
                // Find the point on the regression line
                double x = (predictors[predictors.Length - 1] - predictors[0]) * (i / (MAX - 1.0D)) + predictors[0];
                double y = regression.b1 + regression.b2 * x;

                // Find the t value and standard error
                tval = Statistics.ITCDF((1.0D - Convert.ToDouble(numSignificance.Value)) * 0.5D, predictors.Length - 2);
                double se = Math.Sqrt(regression.Var * ((1.0D / (double)regression.n) + (Math.Pow(x - regression.muX, 2) / regression.ssXX)));

                // Add a point to each side of the confidence band
                pointsLowerConfidence.Add(x, y - tval * se);
                pointsUpperConfidence.Add(x, y + tval * se);
            }

            graph.AddCurve("b2 Confidence Band", pointsLowerConfidence, Color.Blue, SymbolType.None);
            graph.AddCurve("", pointsUpperConfidence, Color.Blue, SymbolType.None);
            ///////////////////////////////

            // Draw a label for the coefficient of determination
            TextItem labelR2 = new TextItem("R-Squared: " + Math.Round(regression.R2 * 100.0D, 1) + "%", 0.15F, 0.20F, CoordType.PaneFraction);
            labelR2.Location.AlignH = AlignH.Left;
            labelR2.Location.AlignV = AlignV.Top;
            labelR2.FontSpec.Border.IsVisible = false;
            labelR2.FontSpec.StringAlignment = StringAlignment.Near;
            graph.GraphItemList.Add(labelR2);
        }

        /// <summary>
        /// Draw a histogram of the residuals from the regression. regression.Calculate() must be ran before 
        /// this function.
        /// </summary>
        /// <param name="graph">GraphPane to draw the histogram on</param>
        private void DrawResidualsHistogram(GraphPane graph)
        {
            if (0 == regression.n) { return; }

            int bins;

            // This algorithm for automatically selecting the number of bins when the sample size 
            // is less than 25 was created only for aesthetic value. For sample sizes greater than 
            // or equal to 25 the rule of thumb sqrt(n) bin count is used.
            if (regression.n < 6)
            {
                bins = regression.n;
            }
            else if (regression.n < 25)
            {
                bins = (int)(2.5D + 0.25D * (double)regression.n);
            }
            else
            {
                bins = (int)Math.Sqrt((double)regression.n) + 2;
            }

            // Copy the residuals in to a new array
            double[] residuals = new double[regression.n];
            for (int i = 0; i < regression.n; i++)
            {
                residuals[i] = regression.Residual(i);
            }
            // Sort the new copy of the array
            Array.Sort(residuals);

            double binSize = (residuals[regression.n - 1] - residuals[0]) / bins;

            double[] frequencies = new double[bins];

            int bin = 1;

            // Populate the frequencies array based on the sorted residual data
            for (int i = 0; i < regression.n; i++)
            {
                while (bin < bins && residuals[i] >= residuals[0] + binSize * bin)
                {
                    bin++;
                }

                frequencies[bin - 1]++;
            }

            // Draw the bar chart (histogram)
            PointPairList bars = new PointPairList();

            bars.Add(residuals[0], 0);
            for (int i = 0; i < bins; i++)
            {
                bars.Add(residuals[0] + (double)i * binSize, frequencies[i]);
                graph.AddCurve("", new double[2] { residuals[0] + (double)i * binSize, residuals[0] + (double)i * binSize },
                               new double[2] { 0, frequencies[i] }, Color.Red, SymbolType.None);
            }
            bars.Add(residuals[residuals.Length - 1], 0);

            LineItem histogram = graph.AddCurve("", bars, Color.Red, SymbolType.None);
            //histogram.Line.Fill = new Fill(Color.White, Color.FromArgb(60, 190, 50), 90F);
            histogram.Line.StepType = StepType.ForwardStep;
        }

        /// <summary>
        /// Draw confidence level lines on the T chart, from the significance level input in numSignificance 
        /// and the b2 value from the regression class member. regression.Calculate() must be ran before this 
        /// function.
        /// </summary>
        /// <param name="graph">GraphPane with a T chart to draw significance levels on</param>
        private void Drawb2Significance(GraphPane graph)
        {
            if (!IsNumeric(numSignificance.Value) || 0 == regression.n)
            {
                graph.Title = "b2 Significance Levels";
                return;
            }

            graph.Title = "b2 Significance Levels (" + (regression.n - 2) + " d.f.)";

            double tval = regression.b2 / regression.SEb2;
            double significance = Convert.ToDouble(numSignificance.Value);

            PointPairList pointsSigLine = new PointPairList();
            pointsSigLine.Add(tval, 0.0D);
            pointsSigLine.Add(tval, Statistics.TPDF(tval, regression.n - 2));
            graphb2TChart.GraphPane.AddCurve("b2 Significance Level", pointsSigLine, Color.Green, SymbolType.Circle);

            double tval2 = Statistics.ITCDF(significance, regression.n - 2);
            double tpdf = Statistics.TPDF(tval2, regression.n - 2);

            pointsSigLine = new PointPairList();
            pointsSigLine.Add(tval2, 0);
            pointsSigLine.Add(tval2, tpdf);
            graphb2TChart.GraphPane.AddCurve("Test Significance Level", pointsSigLine, Color.Blue, SymbolType.None);
            pointsSigLine = new PointPairList();
            pointsSigLine.Add(-tval2, 0);
            pointsSigLine.Add(-tval2, tpdf);
            graphb2TChart.GraphPane.AddCurve("", pointsSigLine, Color.Blue, SymbolType.None);

            // Draw a label showing the critical t value for the b2 coefficient
            TextItem label = new TextItem("b2 T Value: " + Math.Round(tval, 3).ToString(), 
                                          (float)tval, 
                                          (float)(Statistics.TPDF(tval, regression.n - 2) + 0.01D));
            label.Location.AlignH = AlignH.Left;
            label.Location.AlignV = AlignV.Bottom;
            label.FontSpec.Border.IsVisible = false;
            label.FontSpec.StringAlignment = StringAlignment.Near;
            graphb2TChart.GraphPane.GraphItemList.Add(label);
        }

        /// <summary>
        /// Graph a T distribution
        /// </summary>
        /// <param name="graph">GraphPane to draw the T distribution on</param>
        /// <param name="k">Adjusted degrees of freedom. For a univariate regression, n - 2</param>
        private static void DrawTDistribution(GraphPane graph, int k)
        {
            PointPairList pointsTLine = new PointPairList();

            for (float i = -6.0F; i < 6.0F; i += 0.01F)
            {
                pointsTLine.Add(i, Statistics.TPDF(i, k));
            }

            graph.AddCurve("Student T Distribution", pointsTLine, Color.Red, SymbolType.None);
        }

        /// <summary>
        /// Draw a normal distribution
        /// </summary>
        /// <param name="graph">GraphPane to draw the normal distribution on</param>
        private static void DrawNormalDistribution(GraphPane graph)
        {
            PointPairList pointsNormalLine = new PointPairList();

            for (float i = -5.0F; i < 5.0F; i += 0.01F)
            {
                pointsNormalLine.Add(i, Math.Exp(-0.5F * (i * i)) / (Math.Sqrt(2 * Math.PI)) );
            }
            
            graph.AddCurve("Standardized Normal Distribution", pointsNormalLine, Color.Black, SymbolType.None);
        }

        /// <summary>
        /// Called when the DataGridView data is changed, updates all the graphs.
        /// </summary>
        private void DataChanged(object sender, DataGridViewCellEventArgs e)
        {
            LoadRegressionData();

            // Clear and redraw the regression graph
            graphRegression.GraphPane.CurveList.Clear();
            graphRegression.GraphPane.GraphItemList.Clear();
            DrawRegressionData(graphRegression.GraphPane);
            DrawRegressionLine(graphRegression.GraphPane);
            graphRegression.AxisChange();
            graphRegression.Refresh();

            // Clear and redraw the residuals histogram
            graphResiduals.GraphPane.CurveList.Clear();
            DrawResidualsHistogram(graphResiduals.GraphPane);
            graphResiduals.AxisChange();
            graphResiduals.Refresh();

            // Clear and redraw the b2 significance level graph
            graphb2TChart.GraphPane.CurveList.Clear();
            graphb2TChart.GraphPane.GraphItemList.Clear();
            DrawNormalDistribution(graphb2TChart.GraphPane);
            DrawTDistribution(graphb2TChart.GraphPane, pointsData.Count - 2);
            Drawb2Significance(graphb2TChart.GraphPane);
            graphb2TChart.AxisChange();
            graphb2TChart.Refresh();

            // Only allow saving three or more data points
            if (dataGrid.RowCount > 2)
            {
                modified = true;
                menuSave.Enabled = true;
                menuSaveAs.Enabled = true;
            }
            else
            {
                modified = false;
                menuSave.Enabled = false;
                menuSaveAs.Enabled = false;
            }
        }

        /// <summary>
        /// Called when any UI object is modified other than the DataGridView, calls the 
        /// DataGridViews changed method.
        /// </summary>
        private void SamplesChanged(object sender, EventArgs e)
        {
            DataChanged(null, null);
        }

        /// <summary>
        /// Load event for the main form, sets all the graph properties.
        /// </summary>
        private void FormVisualRegression_Load(object sender, EventArgs e)
        {
            dataGrid.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableWithoutHeaderText;

            graphRegression.GraphPane.Title = "Ordinary Least Squares Regression";
            graphRegression.GraphPane.YAxis.Title = "Response";
            graphRegression.GraphPane.XAxis.Title = "Predictor";

            graphResiduals.GraphPane.Title = "Residuals Histogram";
            graphResiduals.GraphPane.YAxis.Title = "Frequency";
            graphResiduals.GraphPane.XAxis.Title = "Value";

            graphb2TChart.GraphPane.Title = "b2 Significance Levels";
            graphb2TChart.GraphPane.YAxis.Title = "";
            graphb2TChart.GraphPane.YAxis.Cross = 0.0D;
            graphb2TChart.GraphPane.XAxis.Title = "Critical Value";
            graphb2TChart.GraphPane.XAxis.IsMinorInsideTic = false;
            graphb2TChart.GraphPane.XAxis.IsInsideTic = false;
            graphb2TChart.GraphPane.XAxis.IsOppositeTic = false;
            graphb2TChart.GraphPane.XAxis.IsMinorOppositeTic = false;
            graphb2TChart.GraphPane.YAxis.IsAllTics = false;
            graphb2TChart.GraphPane.YAxis.IsScaleVisible = false;
            graphb2TChart.GraphPane.YAxis.IsOppositeTic = false;
            graphb2TChart.GraphPane.YAxis.IsMinorOppositeTic = false;
        }

        /// <summary>
        /// DataGridView event when a key is pressed, to handle copy/cut/paste/delete
        /// </summary>
        private void dataGrid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && !e.Shift && !e.Alt && Keys.V == e.KeyCode)
            {
                // Ctrl+V was pressed, handle pasting
                if (Clipboard.ContainsText())
                {
                    if (IsNumeric(Clipboard.GetText()))
                    {
                        dataGrid.CurrentCell.Value = Clipboard.GetText();
                        return;
                    }

                    string[] arrayValues = new string[0];

                    string strPaste = Clipboard.GetText();

                    if (strPaste.Contains("\r\n"))
                    {
                        arrayValues = strPaste.Split(new string[] {"\r\n"}, StringSplitOptions.RemoveEmptyEntries);
                    }
                    else if (strPaste.Contains(","))
                    {
                        arrayValues = strPaste.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    }

                    if (arrayValues.Length > 0)
                    {
                        int index = dataGrid.CurrentRow.Index;
                        
                        if (index + arrayValues.Length > dataGrid.RowCount)
                        {
                            dataGrid.Rows.Add((index + arrayValues.Length + 1) - dataGrid.RowCount);
                        }

                        for (int i = 0; i < arrayValues.Length; i++)
                        {
                            string value = arrayValues[i].Trim(new char[] { ' ', '\t', ',' });

                            if (IsNumeric(value))
                            {
                                dataGrid.Rows[index + i].Cells[dataGrid.CurrentCell.ColumnIndex].Value = value;
                            }
                        }
                    }
                }
            }
            else if (e.Control && !e.Shift && !e.Alt && (Keys.C == e.KeyCode || Keys.X == e.KeyCode))
            {
                // Ctrl+C or Ctrl+X was pressed, handle copying/cutting
                DataGridViewSelectedCellCollection cells = dataGrid.SelectedCells;

                bool cut = (Keys.X == e.KeyCode);

                if (1 == cells.Count)
                {
                    Clipboard.SetText(cells[0].Value.ToString());
                }
                else if (cells.Count > 1)
                {
                    string clipboardString = "";

                    for (int i = 0; i < cells.Count; i++)
                    {
                        clipboardString += cells[i].Value + "\r\n";

                        if (cut)
                        {
                            cells[i].Value = "";
                        }
                    }

                    Clipboard.SetText(clipboardString);
                }
            }
            else if (e.Control && !e.Shift && !e.Alt && Keys.X == e.KeyCode)
            {
                // Ctrl+X was pressed, handle cutting
            }
            else if (Keys.Delete == e.KeyCode || Keys.Back == e.KeyCode)
            {
                // Delete or Backspace was pressed, handle deleting
                DataGridViewSelectedCellCollection cells = dataGrid.SelectedCells;

                for (int i = 0; i < cells.Count; i++)
                {
                    cells[i].Value = "";
                }
            }
        }

        /// <summary>
        /// Clear the DataGridView, offering the user to save first if modified is true.
        /// </summary>
        public void menuNew_Click(object sender, EventArgs e)
        {
            if (modified)
            {
                if (MessageBox.Show("Save changes?", 
                                    "Visual Regression", 
                                    MessageBoxButtons.YesNo, 
                                    MessageBoxIcon.Question) 
                    == DialogResult.Yes)
                {
                    menuSave_Click(null, null);
                }
            }

            dataGrid.Rows.Clear();
            DataChanged(null, null);
        }

        /// <summary>
        /// Open a .txt file, parsing for columns of numbers
        /// </summary>
        private void menuOpenTXT_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();

            openFile.Filter = "Supported formats (*.txt, *.csv)|*.txt; *.csv";
            openFile.Title = "Open Visual Regression File";

            if (openFile.ShowDialog() == DialogResult.Cancel) { return; }

            try
            {
                FileStream fileStream = new FileStream(openFile.FileName, FileMode.Open, FileAccess.Read);
                StreamReader file = new StreamReader(fileStream);
                int totalColumns = 0;

                // Assume that the file open worked and is going to succeed, 
                // so create the file preview dialog
                FormFileImport FormImport = new FormFileImport();
                FormImport.arrayRows = new ArrayList();
                FormImport.FormMain = this;

                while (file.Peek() >= 0)
                {
                    string line = file.ReadLine();
                    ArrayList arrayColumns = new ArrayList();
                    int pos = 0;
                    bool delimiter = true;
                    string number = "";
                    char chr = '\0';

                    while (pos < line.Length)
                    {
                        chr = line[pos];

                        if (delimiter && IsNumeric(chr) && '.' != chr)
                        {
                            number = chr.ToString();
                            delimiter = false;
                        }
                        else if (!delimiter && IsDelimiter(chr))
                        {
                            delimiter = true;

                            if (number.Length > 0)
                            {
                                arrayColumns.Add(Convert.ToDouble(number));
                            }
                        }
                        else if (pos + 1 == line.Length && number.Length > 0)
                        {
                            if (IsNumeric(chr)) { number += chr.ToString(); }
                            
                            arrayColumns.Add(Convert.ToDouble(number));
                        }
                        else if (number.Length > 0 && IsNumeric(chr))
                        {
                            number += chr.ToString();
                        }
                        else if (!delimiter && number.Length > 0 && !IsNumeric(chr))
                        {
                            // Found a number alongside non-numeric, non-delimiter characters. Ignore.
                            delimiter = true;
                            number = "";
                        }

                        pos++;
                    }

                    // Add this line to the file preview dialog
                    FormImport.txtFileDisplay.Text += line + "\r\n";

                    // Add this row to the array of rows
                    if (arrayColumns.Count > 1) { FormImport.arrayRows.Add(arrayColumns); }

                    // Keep track of the total columns in this file
                    if (arrayColumns.Count > totalColumns) { totalColumns = arrayColumns.Count; }
                }

                file.Close();
                fileStream.Close();

                if (FormImport.arrayRows.Count > 0)
                {
                    for (int i = 0; i < totalColumns; i++)
                    {
                        FormImport.cboResponse.Items.Add(i + 1);
                        FormImport.cboPredictor.Items.Add(i + 1);
                    }

                    FormImport.cboResponse.SelectedIndex = 0;
                    FormImport.cboPredictor.SelectedIndex = 1;

                    FormImport.FileName = openFile.FileName;

                    FormImport.Show(this);
                }
                else
                {
                    MessageBox.Show("Couldn't find any data columns in " + openFile.FileName, "Import File",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error opening file " + openFile.FileName, "Import File", 
                                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        /// <summary>
        /// Save the current data to filename. Call menuSaveAs_Click() if filename is empty.
        /// </summary>
        private void menuSave_Click(object sender, EventArgs e)
        {
            if (fileName.Length > 0)
            {
                string separator = (fileName.EndsWith(".csv")) ? "," : "\t";

                try
                {
                    FileStream fileStream = new FileStream(fileName, FileMode.Create, FileAccess.Write);
                    StreamWriter file = new StreamWriter(fileStream);

                    for (int i = 0; i < dataGrid.Rows.Count; i++)
                    {
                        if (IsNumeric(dataGrid.Rows[i].Cells[0].Value) &&
                        IsNumeric(dataGrid.Rows[i].Cells[1].Value))
                        {
                            file.WriteLine(dataGrid.Rows[i].Cells[0].Value + separator + dataGrid.Rows[i].Cells[1].Value);
                        }
                    }

                    file.Close();
                    fileStream.Close();
                }
                catch (Exception)
                {
                    MessageBox.Show("Error saving to file " + fileName, "Save File",
                            MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else
            {
                menuSaveAs_Click(null, null);
            }

            modified = false;
            menuSave.Enabled = false;
            menuSaveAs.Enabled = true;
        }

        /// <summary>
        /// Show a SaveFileDialog to set filename and call menuSave_Click().
        /// </summary>
        private void menuSaveAs_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = "Text file (*.txt)|*.txt|CSV file (*.csv)|*.csv";

            if (saveFile.ShowDialog() == DialogResult.OK)
            {
                fileName = saveFile.FileName;

                modified = true;
                menuSave.Enabled = true;
                menuSaveAs.Enabled = true;

                menuSave_Click(sender, e);
            }
        }

        /// <summary>
        /// Ask to save changes if modified is true, and exit the application.
        /// </summary>
        private void menuExit_Click(object sender, EventArgs e)
        {
            if (modified)
            {
                if (MessageBox.Show("Save changes?",
                                    "Visual Regression",
                                    MessageBoxButtons.YesNo,
                                    MessageBoxIcon.Question)
                    == DialogResult.Yes)
                {
                    menuSave_Click(null, null);
                }
            }

            Application.Exit();
        }

        /// <summary>
        /// Show an about dialog.
        /// </summary>
        private void menuAbout_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Visual Regression\n\nWritten by John Hurliman (jhurliman@wsu.edu)\n" + 
                            "Contains the ZedGraph charting library available at http://zedgraph.sourceforge.net/\n" +
                            "Portions of this software have been adapted from the Cephes library available at " +
                            "http://www.netlib.org/cephes/\n", "About Visual Regression");
        }
	}
}
