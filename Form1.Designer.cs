namespace CourseWork90
{
    partial class FormMain
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
            pictureBoxMain = new PictureBox();
            SelectColorComboBox = new ComboBox();
            SelectTMOComboBox = new ComboBox();
            SelectFigureComboBox = new ComboBox();
            GTComboBox = new ComboBox();
            TakeFigure = new CheckBox();
            ButtonForTMO = new Button();
            PaintCubSpline = new Button();
            ToPaint = new Button();
            DeleteFigure_ = new Button();
            ButtonClear_ = new Button();
            label1 = new Label();
            Angle = new TextBox();
            numericForPolinom = new NumericUpDown();
            label2 = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBoxMain).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericForPolinom).BeginInit();
            SuspendLayout();
            // 
            // pictureBoxMain
            // 
            pictureBoxMain.BackColor = SystemColors.Window;
            pictureBoxMain.BorderStyle = BorderStyle.FixedSingle;
            pictureBoxMain.Location = new Point(0, 0);
            pictureBoxMain.Name = "pictureBoxMain";
            pictureBoxMain.Size = new Size(1051, 596);
            pictureBoxMain.TabIndex = 0;
            pictureBoxMain.TabStop = false;
            pictureBoxMain.MouseDown += PictureBoxMouseDown;
            pictureBoxMain.MouseMove += PictureBoxMouseMove;
            // 
            // SelectColorComboBox
            // 
            SelectColorComboBox.FormattingEnabled = true;
            SelectColorComboBox.Items.AddRange(new object[] { "Черный", "Красный", "Синий", "Зеленый" });
            SelectColorComboBox.Location = new Point(12, 602);
            SelectColorComboBox.Name = "SelectColorComboBox";
            SelectColorComboBox.Size = new Size(121, 23);
            SelectColorComboBox.TabIndex = 1;
            SelectColorComboBox.Text = "Цвет";
            SelectColorComboBox.SelectedIndexChanged += SelectColor;
            // 
            // SelectTMOComboBox
            // 
            SelectTMOComboBox.FormattingEnabled = true;
            SelectTMOComboBox.Items.AddRange(new object[] { "Объединение", "Пересечение", "Симметрическая разность", "Разность А/В", "Разность В/А" });
            SelectTMOComboBox.Location = new Point(139, 602);
            SelectTMOComboBox.Name = "SelectTMOComboBox";
            SelectTMOComboBox.Size = new Size(173, 23);
            SelectTMOComboBox.TabIndex = 2;
            SelectTMOComboBox.Text = "Операция ТМО";
            SelectTMOComboBox.SelectedIndexChanged += SelectTMOComboBox_;
            // 
            // SelectFigureComboBox
            // 
            SelectFigureComboBox.FormattingEnabled = true;
            SelectFigureComboBox.Items.AddRange(new object[] { "Фигура №3", "Флаг" });
            SelectFigureComboBox.Location = new Point(318, 602);
            SelectFigureComboBox.Name = "SelectFigureComboBox";
            SelectFigureComboBox.Size = new Size(121, 23);
            SelectFigureComboBox.TabIndex = 3;
            SelectFigureComboBox.Text = "Выбор фигуры";
            SelectFigureComboBox.SelectedIndexChanged += SelectFigure;
            // 
            // GTComboBox
            // 
            GTComboBox.FormattingEnabled = true;
            GTComboBox.Items.AddRange(new object[] { "Перемещение", "Поворот вокруг заданного центра на произвольный угол", "Зеркальное отражение относительно горизонтальной прямой", "Зеркальное отражение относительно центра фигуры" });
            GTComboBox.Location = new Point(445, 602);
            GTComboBox.Name = "GTComboBox";
            GTComboBox.Size = new Size(396, 23);
            GTComboBox.TabIndex = 4;
            GTComboBox.Text = "Геометрические преобразования";
            GTComboBox.SelectedIndexChanged += SelectGt;
            // 
            // TakeFigure
            // 
            TakeFigure.AutoSize = true;
            TakeFigure.Location = new Point(927, 636);
            TakeFigure.Name = "TakeFigure";
            TakeFigure.Size = new Size(124, 19);
            TakeFigure.TabIndex = 5;
            TakeFigure.Text = "Фигура выбрана";
            TakeFigure.UseVisualStyleBackColor = true;
            // 
            // ButtonForTMO
            // 
            ButtonForTMO.Location = new Point(1057, 12);
            ButtonForTMO.Name = "ButtonForTMO";
            ButtonForTMO.Size = new Size(130, 35);
            ButtonForTMO.TabIndex = 6;
            ButtonForTMO.Text = "Выполнить ТМО";
            ButtonForTMO.UseVisualStyleBackColor = true;
            ButtonForTMO.Click += ButtonForTMO_Click;
            // 
            // PaintCubSpline
            // 
            PaintCubSpline.Location = new Point(1057, 135);
            PaintCubSpline.Name = "PaintCubSpline";
            PaintCubSpline.Size = new Size(130, 56);
            PaintCubSpline.TabIndex = 7;
            PaintCubSpline.Text = "Нарисовать кубический сплайн";
            PaintCubSpline.UseVisualStyleBackColor = true;
            PaintCubSpline.Click += PaintCubeSpline_Click;
            // 
            // ToPaint
            // 
            ToPaint.Location = new Point(1057, 269);
            ToPaint.Name = "ToPaint";
            ToPaint.Size = new Size(130, 58);
            ToPaint.TabIndex = 8;
            ToPaint.Text = "Перейти в режим свободного рисования";
            ToPaint.UseVisualStyleBackColor = true;
            ToPaint.Click += ToPaint_Click;
            // 
            // DeleteFigure_
            // 
            DeleteFigure_.Location = new Point(1057, 428);
            DeleteFigure_.Name = "DeleteFigure_";
            DeleteFigure_.Size = new Size(130, 35);
            DeleteFigure_.TabIndex = 9;
            DeleteFigure_.Text = "Удаление фигур";
            DeleteFigure_.UseVisualStyleBackColor = true;
            DeleteFigure_.Click += DeleteFigure_Click;
            // 
            // ButtonClear_
            // 
            ButtonClear_.Location = new Point(1057, 534);
            ButtonClear_.Name = "ButtonClear_";
            ButtonClear_.Size = new Size(130, 35);
            ButtonClear_.TabIndex = 10;
            ButtonClear_.Text = "Очистка экрана";
            ButtonClear_.UseVisualStyleBackColor = true;
            ButtonClear_.Click += ButtonClear_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(847, 608);
            label1.Name = "label1";
            label1.Size = new Size(98, 15);
            label1.TabIndex = 11;
            label1.Text = "Угол поворота";
            // 
            // Angle
            // 
            Angle.Location = new Point(951, 605);
            Angle.Name = "Angle";
            Angle.ReadOnly = true;
            Angle.Size = new Size(100, 23);
            Angle.TabIndex = 12;
            // 
            // numericForPolinom
            // 
            numericForPolinom.Location = new Point(263, 635);
            numericForPolinom.Maximum = new decimal(new int[] { 250, 0, 0, 0 });
            numericForPolinom.Minimum = new decimal(new int[] { 50, 0, 0, 0 });
            numericForPolinom.Name = "numericForPolinom";
            numericForPolinom.Size = new Size(55, 23);
            numericForPolinom.TabIndex = 13;
            numericForPolinom.Value = new decimal(new int[] { 50, 0, 0, 0 });
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 638);
            label2.Name = "label2";
            label2.Size = new Size(245, 15);
            label2.TabIndex = 14;
            label2.Text = "Размер ребра для создания полинома";
            // 
            // FormMain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaption;
            ClientSize = new Size(1195, 662);
            Controls.Add(label2);
            Controls.Add(numericForPolinom);
            Controls.Add(Angle);
            Controls.Add(label1);
            Controls.Add(ButtonClear_);
            Controls.Add(DeleteFigure_);
            Controls.Add(ToPaint);
            Controls.Add(PaintCubSpline);
            Controls.Add(ButtonForTMO);
            Controls.Add(TakeFigure);
            Controls.Add(GTComboBox);
            Controls.Add(SelectFigureComboBox);
            Controls.Add(SelectTMOComboBox);
            Controls.Add(SelectColorComboBox);
            Controls.Add(pictureBoxMain);
            Font = new Font("Consolas", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            Name = "FormMain";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Курсовая работа";
            ((System.ComponentModel.ISupportInitialize)pictureBoxMain).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericForPolinom).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBoxMain;
        private ComboBox SelectColorComboBox;
        private ComboBox SelectTMOComboBox;
        private ComboBox SelectFigureComboBox;
        private ComboBox GTComboBox;
        private CheckBox TakeFigure;
        private Button ButtonForTMO;
        private Button PaintCubSpline;
        private Button ToPaint;
        private Button DeleteFigure_;
        private Button ButtonClear_;
        private Label label1;
        private TextBox Angle;
        private NumericUpDown numericForPolinom;
        private Label label2;
    }
}