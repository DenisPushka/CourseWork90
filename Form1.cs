namespace CourseWork90
{
    /// <summary>
    /// Главная форма.
    /// </summary>
    public partial class FormMain: Form
    {
        private readonly Graphics _graphics;
        private readonly Pen _drawPen = new(Color.Black, 1);
        private Figure _figure;

        /// <summary>
        /// Множество для ТМО
        /// </summary>
        private int[] _setQ = new int[2];

        /// <summary>
        /// Список фигур
        /// </summary>
        private readonly List<Figure> _figures = new();

        /// <summary>
        /// Буфер
        /// </summary>
        private readonly Bitmap _bitmap;

        /// <summary>
        /// Выбор геометрической операции
        /// </summary>
        private int _operation;

        #region For move figure

        private bool _checkFigure;
        private Point _pictureBoxMousePosition;
        private Figure _figureForMove;

        #endregion

        public FormMain()
        {
            InitializeComponent();
            _bitmap = new Bitmap(pictureBoxMain.Width, pictureBoxMain.Height);
            _graphics = Graphics.FromImage(_bitmap);
            _figure = new Figure(pictureBoxMain.Width, pictureBoxMain.Height, _graphics, _drawPen, _drawPen.Color);
            _operation = -1;
            MouseWheel += GeometricTransformation;
        }

        /// <summary>
        /// Обработчик события "нажатие мыши"
        /// </summary>
        private void PictureBoxMouseDown(object sender, MouseEventArgs e)
        {
            _pictureBoxMousePosition = e.Location;
            switch (_operation)
            {
                // Удаление
                case -3:
                    if (SearchSelectFigure(e))
                    {
                        if (_figures.Any(figure => figure == _figureForMove))
                        {
                            _figures.Remove(_figureForMove);
                        }
                    }
                    else
                        _checkFigure = false;

                    break;
                // Рисование
                // Рисование примитива
                case -2:
                    switch (SelectFigureComboBox.SelectedIndex)
                    {
                        case 0:
                            CreateFigure3(e);
                            break;
                        case 1:
                            CreateRhomb(e);
                            break;
                    }

                    _figure.Color = _drawPen.Color;
                    _figure.DrawPen = _drawPen;
                    _figures.Add(_figure.Cloning());
                    _figure = new Figure(pictureBoxMain.Width, pictureBoxMain.Height, _graphics,
                        new Pen(_drawPen.Color), _drawPen.Color);
                    break;
                // Добавление точки
                case -1 when e.Button == MouseButtons.Left:
                    {
                        _figure.AddVertex(e.X, e.Y);
                        break;
                    }
                // Создание фигуры
                case -1 when e.Button == MouseButtons.Right:
                    {
                        _figure.DrawPen = _drawPen;
                        _figures.Add(_figure.Cloning());
                        _figure = new Figure(pictureBoxMain.Width, pictureBoxMain.Height, _graphics,
                            new Pen(_drawPen.Color), _drawPen.Color);
                        break;
                    }
                // Перемещение
                case 0:
                    if (SearchSelectFigure(e))
                    {
                        _graphics.DrawEllipse(new Pen(Color.Blue), e.X - 2, e.Y - 2, 10, 10);
                        _checkFigure = true;
                    }
                    else
                        _checkFigure = false;

                    break;
            }

            if (SearchSelectFigure(e))
            {
                TakeFigure.Checked = true;
            }

            Render();
        }

        /// <summary>
        /// Рендеринг.
        /// </summary>
        private void Render()
        {
            _graphics.Clear(Color.White);

            if (_operation == -1)
            {
                _figure.PaintingLineInFigure();
            }

            foreach (var fig in _figures)
            {
                if (!fig.IsHaveTmo)
                {
                    fig.Fill();
                }
                else
                {
                    Tmo(fig, fig.ConnectAfterTmo);
                }
            }

            pictureBoxMain.Image = _bitmap;
        }

        /// <summary>
        /// Обработчик события "Двигать мышь".
        /// </summary>
        private void PictureBoxMouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && _operation == 0 & _checkFigure)
            {
                Render();

                if (_figureForMove.IsHaveTmo)
                {
                    _figureForMove.Move(e.X - _pictureBoxMousePosition.X, e.Y - _pictureBoxMousePosition.Y);
                    _figureForMove.ConnectAfterTmo.Move(e.X - _pictureBoxMousePosition.X,
                        e.Y - _pictureBoxMousePosition.Y);

                    Tmo(_figureForMove, _figureForMove.ConnectAfterTmo);
                    pictureBoxMain.Image = _bitmap;
                }
                else
                {
                    _figureForMove.Move(e.X - _pictureBoxMousePosition.X, e.Y - _pictureBoxMousePosition.Y);
                }

                _pictureBoxMousePosition = e.Location;
            }
        }

        /// <summary>
        /// Обработчик события на прокручивания колесика при геометрических преобразованиях.
        /// </summary>
        private void GeometricTransformation(object sender, MouseEventArgs e)
        {
            if (_figures.Count == 0 || !TakeFigure.Checked)
            {
                return;
            }

            if (_figureForMove.IsHaveTmo)
            {
                OperationGeometric(_figureForMove, e);
                OperationGeometric(_figureForMove.ConnectAfterTmo, e);
                PaintTmo(_figureForMove);
            }
            else
                OperationGeometric(_figureForMove, e);

            Render();
        }

        /// <summary>
        /// ТМО для 2 объектов.
        /// </summary>
        private void PaintTmo(Figure figureBuff)
        {
            figureBuff.DrawPen.Color = figureBuff.Color =
                figureBuff.ConnectAfterTmo.DrawPen.Color = figureBuff.ConnectAfterTmo.Color = Color.White;

            Tmo(figureBuff, figureBuff.ConnectAfterTmo);
        }

        /// <summary>
        /// Геометрические преобразования.
        /// </summary>
        private void OperationGeometric(Figure figureBuff, MouseEventArgs e)
        {
            switch (_operation)
            {
                // Вращение.
                case 1:
                    figureBuff.Rotation(e, Angle);
                    break;
                // Отражение.
                case 2:
                    figureBuff.Mirror(e, false);
                    break;
                case 3:
                    figureBuff.Mirror(e, true);
                    break;
            }

            Render();
        }

        /// <summary>
        /// Поиск пересекаемых фигур. Если есть несколько фигур, то возвращается первая пересеченная фигура.
        /// </summary>
        /// <param name="figure">Основная фигура.</param>
        /// <returns>Фигура, которая имеет пересечение с фигурой, переданной в параметр. Если такой нет, то возвращается null/</returns>
        private Figure? SearchIntersection(Figure figure)
        {
            foreach (var t in _figures)
                if (CheckInputVertexFirstFigureToSecondFigure(figure, t)
                    || CheckInputVertexFirstFigureToSecondFigure(t, figure))
                {
                    if (figure.Equals(t))
                        continue;
                    return t;
                }

            return null;
        }

        /// <summary>
        /// Проверка пересения фигугры1 с фигурой 2 по вхождению вершин фигуры1 в фигуру 2
        /// </summary>
        /// <param name="figureOne">Фигура 1</param>
        /// <param name="figureSecond">Фигура 2</param>
        private static bool CheckInputVertexFirstFigureToSecondFigure(Figure figureOne, Figure figureSecond) =>
            figureOne.Vertexes
                .Any(vertex => figureSecond
                    .ThisFigure((int)vertex.X, (int)vertex.Y));

        /// <summary>
        /// Поиск фигуры в координате <paramref name="e"/>.
        /// </summary>
        /// <param name="e">Координата мыши.</param>
        /// <returns>True - в случае успеха.</returns>
        private bool SearchSelectFigure(MouseEventArgs e)
        {
            for (var index = 0; index < _figures.Count; index++)
                if (_figures[index].ThisFigure(e.X, e.Y))
                {
                    _figureForMove = _figures[index];
                    _figureForMove.IndexMove = index;
                    return true;
                }

            return false;
        }

        private void SelectFigure(object sender, EventArgs e) => _operation = -2;

        private void SelectGt(object sender, EventArgs e) => _operation = GTComboBox.SelectedIndex;

        /// <summary>
        /// Обработчик события нажатие на кнопку "Переход в свободное рисование".
        /// </summary>
        private void ToPaint_Click(object sender, EventArgs e) => _operation = -1;

        /// <summary>
        /// Обработчик события, выбор цвета.
        /// </summary>
        private void SelectColor(object sender, EventArgs e) =>
            _drawPen.Color = SelectColorComboBox.SelectedIndex switch
            {
                0 => Color.Black,
                1 => Color.Red,
                2 => Color.Blue,
                3 => Color.Green,
                _ => _drawPen.Color
            };

        #region Построение фигур и функций

        /// <summary>
        /// Создание фигуры - "Фигуры 3": "Бантик".
        /// </summary>
        private void CreateFigure3(MouseEventArgs e)
        {
            var size = (float)numericForPolinom.Value;

            _figure.Vertexes = new List<Vertex>
            {
                new(e.X - (int)(size * 2.5), e.Y - (int)(size * 1.5)),
                new(e.X - size, e.Y - (int)(size * 0.5)),
                new(e.X + size, e.Y - (int)(size * 0.5)),
                new(e.X + (int)(size * 2.5), e.Y - (int)(size * 1.5)),
                new(e.X + (int)(size * 2.5), e.Y + (int)(size * 1.5)),
                new(e.X + size, e.Y + (int)(size * 0.5)),
                new(e.X - size, e.Y + (int)(size * 0.5)),
                new(e.X - (int)(size * 2.5), e.Y + (int)(size * 1.5)),
            };
        }

        /// <summary>
        /// Создание фигуры - "Флаг".
        /// </summary>
        private void CreateRhomb(MouseEventArgs e)
        {
            var size = (float)numericForPolinom.Value;

            _figure.Vertexes = new List<Vertex>
            {
                new(e.X - size, e.Y - size),
                new(e.X + (int)(size * 1.5), e.Y - size),
                new(e.X, e.Y),
                new(e.X + (int)(size * 1.5), e.Y + size),
                new(e.X - size, e.Y + size),
            };
        }

        /// <summary>
        /// Кубический сплайн.
        /// </summary>
        private void CubeSpline()
        {
            var function = new List<Vertex>();
            var l = new PointF[4];
            var pv1 = _figure.Vertexes[0].ToPoint();
            var pv2 = _figure.Vertexes[0].ToPoint();

            const double dt = 0.04;
            double t = 0;
            Point pred = _figure.Vertexes[0].ToPoint(), pt = _figure.Vertexes[0].ToPoint();

            pv1.X = (int)(4 * (_figure.Vertexes[1].X - _figure.Vertexes[0].X));
            pv1.Y = (int)(4 * (_figure.Vertexes[1].Y - _figure.Vertexes[0].Y));
            pv2.X = (int)(4 * (_figure.Vertexes[3].X - _figure.Vertexes[2].X));
            pv2.Y = (int)(4 * (_figure.Vertexes[3].Y - _figure.Vertexes[2].Y));

            l[0].X = 2 * _figure.Vertexes[0].X - 2 * _figure.Vertexes[2].X + pv1.X + pv2.X; // Ax
            l[0].Y = 2 * _figure.Vertexes[0].Y - 2 * _figure.Vertexes[2].Y + pv1.Y + pv2.Y; // Ay
            l[1].X = -3 * _figure.Vertexes[0].X + 3 * _figure.Vertexes[2].X - 2 * pv1.X - pv2.X; // Bx
            l[1].Y = -3 * _figure.Vertexes[0].Y + 3 * _figure.Vertexes[2].Y - 2 * pv1.Y - pv2.Y; // By
            l[2].X = pv1.X; // Cx
            l[2].Y = pv1.Y; // Cy
            l[3].X = _figure.Vertexes[0].X; // Dx 
            l[3].Y = _figure.Vertexes[0].Y; // Dy 
            function.Add(new Vertex(pred.X, pred.Y));
            while (t < 1 + dt / 2)
            {
                var xt = ((l[0].X * t + l[1].X) * t + l[2].X) * t + l[3].X;
                var yt = ((l[0].Y * t + l[1].Y) * t + l[2].Y) * t + l[3].Y;

                pt.X = (int)Math.Round(xt);
                pt.Y = (int)Math.Round(yt);

                _graphics.DrawLine(_drawPen, pred, pt);
                pred = pt;
                function.Add(new Vertex(pred.X, pred.Y));
                t += dt;
            }

            _figure.Vertexes = function;
        }

        /// <summary>
        /// Вычисление полинома.
        /// </summary>
        private static double Polinom(int i, int n, float t) =>
        Factorial(n) / (Factorial(i) * Factorial(n - i))
        * (float)Math.Pow(t, i) *
        (float)Math.Pow(1 - t, n - i);

        /// <summary>
        /// Вычисление факториала.
        /// </summary>
        /// <param name="n">До куда считать.</param>
        /// <returns>Вычисленный факториал.</returns>
        private static double Factorial(int n)
        {
            double x = 1;
            for (var i = 1; i <= n; i++)
                x *= i;

            return x;
        }

        #endregion

        private void PaintCubeSpline_Click(object sender, EventArgs e)
        {
            if (_figure.Vertexes.Count > 3)
            {
                // Создание куб сплайна
                CubeSpline();

                // Добавление его в список фигур.
                _figure.IsFunction = true;
                _figures.Add(_figure.Cloning());
                _figure = new Figure(pictureBoxMain.Width, pictureBoxMain.Height, _graphics, _drawPen, _drawPen.Color);

                Render();
            }
        }

        private void ButtonClear_Click(object sender, EventArgs e)
        {
            _figures.Clear();
            _figure = new Figure(pictureBoxMain.Width, pictureBoxMain.Height, _graphics, new Pen(_drawPen.Color),
                _drawPen.Color);
            pictureBoxMain.Image = _bitmap;
            _graphics.Clear(Color.White);
            _operation = -1;
        }

        private void DeleteFigure_Click(object sender, EventArgs e) => _operation = -3;

        #region ТМО

        /// <summary>
        /// Алгоритм теоретико-множественных операций
        /// </summary>
        /// <param name="figure1">Первая фигура для ТМО.</param>
        /// <param name="figure2">Вторая фигура для ТМО.</param>
        private void Tmo(Figure figure1, Figure figure2)
        {
            var arr = figure1.SearchYMinAndMax();
            var arr2 = figure2.SearchYMinAndMax();

            figure1.IsHaveTmo = figure2.IsHaveTmo = true;
            var minY = arr[0] < arr2[0] ? arr[0] : arr2[0];
            var maxY = arr[1] > arr2[1] ? arr[1] : arr2[1];

            for (var y = (int)minY; y < maxY; y++)
            {
                var a = figure1.CalculationListXrAndXl(y);
                var xAl = a[0];
                var xAr = a[1];
                var b = figure2.CalculationListXrAndXl(y);
                var xBl = b[0];
                var xBr = b[1];
                if (xAl.Count == 0 && xBl.Count == 0)
                    continue;

                #region Заполнение массива arrayM

                var arrayM = new M[xAl.Count * 2 + xBl.Count * 2];
                for (var i = 0; i < xAl.Count; i++)
                    arrayM[i] = new M(xAl[i], 2);

                var nM = xAl.Count;
                for (var i = 0; i < xAr.Count; i++)
                    arrayM[nM + i] = new M(xAr[i], -2);

                nM += xAr.Count;
                for (var i = 0; i < xBl.Count; i++)
                    arrayM[nM + i] = new M(xBl[i], 1);

                nM += xBl.Count;
                for (var i = 0; i < xBr.Count; i++)
                    arrayM[nM + i] = new M(xBr[i], -1);
                nM += xBr.Count;

                #endregion

                // Сортировка.
                SortArrayM(arrayM);

                var q = 0;
                var xrl = new List<int>();
                var xrr = new List<int>();

                // Особый случай для правой границы сегмента.
                if (arrayM[0].X >= 0 && arrayM[0].Dq < 0)
                {
                    xrl.Add(0);
                    q = -arrayM[1].Dq;
                }

                for (var i = 0; i < nM; i++)
                {
                    var x = arrayM[i].X;
                    var qNew = q + arrayM[i].Dq;
                    if (!IncludeQInSetQ(q) && IncludeQInSetQ(qNew))
                        xrl.Add((int)x);
                    else if (IncludeQInSetQ(q) && !IncludeQInSetQ(qNew))
                        xrr.Add((int)x);

                    q = qNew;
                }

                // Если не найдена правая граница последнего сегмента.
                if (IncludeQInSetQ(q))
                    xrr.Add(pictureBoxMain.Height);

                for (var i = 0; i < xrr.Count; i++)
                    _graphics.DrawLine(_drawPen, new Point(xrr[i], y), new Point(xrl[i], y));
            }
        }

        /// <summary>
        /// Проверка вхождения Q в множество setQ.
        /// </summary>
        /// <param name="q">Проверяемое число.</param>
        /// <returns>True - в случае успеха.</returns>
        private bool IncludeQInSetQ(int q) => _setQ[0] <= q && q <= _setQ[1];

        /// <summary>
        /// Сортировка по Х.
        /// </summary>
        private static void SortArrayM(IList<M> arrayM)
        {
            for (var write = 0; write < arrayM.Count; write++)
                for (var sort = 0; sort < arrayM.Count - 1; sort++)
                    if (arrayM[sort].X > arrayM[sort + 1].X)
                    {
                        var buff = new M(arrayM[sort + 1].X, arrayM[sort + 1].Dq);
                        arrayM[sort + 1] = arrayM[sort];
                        arrayM[sort] = buff;
                    }
        }

        /// <summary>
        /// Обработчик события нажатия на кнопку "Применить выбранное ТМО".
        /// </summary>
        private void ButtonForTMO_Click(object sender, EventArgs e)
        {
            if (_figures.Count > 1 && TakeFigure.Checked)
            {
                var secondFigure = SearchIntersection(_figureForMove);

                if (secondFigure == null || secondFigure.IsHaveTmo)
                {
                    return;
                }

                _figureForMove.Color = secondFigure.Color = Color.White;
                _figureForMove.Fill();
                secondFigure.Fill();

                Tmo(_figureForMove, secondFigure);
                _figureForMove.ConnectAfterTmo = secondFigure;
                secondFigure.ConnectAfterTmo = _figureForMove;
            }

            pictureBoxMain.Image = _bitmap;
        }

        /// <summary>
        /// Выбор ТМО.
        /// </summary>
        private void SelectTMOComboBox_(object sender, EventArgs e) =>
           _setQ = SelectTMOComboBox.SelectedIndex switch
           {
               0 => new[] { 1, 3 }, // Объединение.
               1 => new[] { 3, 3 }, // Пересечение.
               2 => new[] { 1, 2 }, // Симметричная разность.
               3 => new[] { 2, 2 }, // Разность А/В.
               4 => new[] { 1, 1 }, // Разность В/А.
               _ => _setQ
           };

        #endregion
    }
}