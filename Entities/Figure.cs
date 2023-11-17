namespace CourseWork90
{
    /// <summary>
    /// Фигура.
    /// </summary>
    public class Figure
    {
        /// <summary>
        /// Вершины.
        /// </summary>
        public List<Vertex> Vertexes { get; set; }

        /// <summary>
        /// Объект есть функция.
        /// </summary>
        public bool IsFunction { get; set; }

        /// <summary>
        /// Произведено ли ТМО над фигурой.
        /// </summary>
        public bool IsHaveTmo { get; set; }

        /// <summary>
        /// При выполнении ТМО, связынный объект.
        /// </summary>
        public Figure ConnectAfterTmo { get; set; }

        /// <summary>
        /// Цвет.
        /// </summary>
        public Color Color { get; set; }

        /// <summary>
        /// Индекс для перемещения.
        /// </summary>
        public int IndexMove { get; set; } = -1;

        /// <summary>
        /// Предоставляет методы для рисования объектов на устройстве отображения. 
        /// </summary>
        private Graphics Graphics { get; }

        /// <summary>
        /// Определяет объект, используемый для рисования прямых линий и кривых. 
        /// </summary>
        public Pen DrawPen { get; set; }

        /// <summary>
        /// Ширина.
        /// </summary>
        private readonly int _width;

        /// <summary>
        /// Высота.
        /// </summary>
        private readonly int _height;

        public bool Equals(Figure other) =>
            Equals(Vertexes, other.Vertexes) && IsFunction == other.IsFunction && IsHaveTmo == other.IsHaveTmo;

        public Figure(int width, int height, Graphics graphics, Pen drawPen, Color color)
        {
            Vertexes = new List<Vertex>();
            _width = width;
            _height = height;
            Graphics = graphics;
            DrawPen = drawPen;
            Color = color;
        }

        public Figure(int width, int height, List<Vertex> vertexes, Graphics graphics, Pen drawPen)
        {
            _width = width;
            _height = height;
            Vertexes = vertexes;
            Graphics = graphics;
            DrawPen = drawPen;
        }

        /// <summary>
        /// Добавление вершины.
        /// </summary>
        /// <param name="x">Координата по оси Х.</param>
        /// <param name="y">Координата по оси У.</param>
        public void AddVertex(float x, float y)
        {
            Vertexes.Add(new Vertex(x, y));
            Graphics.DrawEllipse(new Pen(Color.Blue), x - 2, y - 2, 10, 10);
            if (Vertexes.Count > 1)
                Graphics.DrawLine(new Pen(Color, DrawPen.Width), Vertexes[Vertexes.Count - 2].ToPoint(),
                    Vertexes[Vertexes.Count - 1].ToPoint());
        }

        /// <summary>
        /// Клонирование.
        /// </summary>
        public Figure Cloning() => new(_width, _height, Vertexes.ToList(), Graphics, new Pen(Color, DrawPen.Width))
        { IsFunction = IsFunction, IsHaveTmo = IsHaveTmo, Color = Color };

        #region Все для закрашивания

        /// <summary>
        /// Алгоритм закрашивания внутри многоугольника.
        /// </summary>
        public void Fill()
        {
            if (IsFunction)
            {
                PaintingLineInFigure();
                return;
            }

            var arr = SearchYMinAndMax();
            var min = arr[0];
            var max = arr[1];
            var xs = new List<float>();

            for (var y = (int)min; y < max; y++)
            {
                var k = 0;
                for (var i = 0; i < Vertexes.Count - 1; i++)
                {
                    k = i < Vertexes.Count ? i + 1 : 1;
                    xs = CheckIntersection(xs, i, k, y);
                }

                xs = CheckIntersection(xs, k, 0, y);
                xs.Sort();

                for (var i = 0; i + 1 < xs.Count; i += 2)
                    Graphics.DrawLine(new Pen(Color, DrawPen.Width), new Point((int)xs[i], y), new Point((int)xs[i + 1], y));

                xs.Clear();
            }
        }

        /// <summary>
        /// Высчитывание списка координат по оси Х для левой и правой границы.
        /// </summary>
        /// <param name="y">Координата оси У.</param>
        /// <returns>Список координат (float).</returns>
        public List<float>[] CalculationListXrAndXl(int y)
        {
            var k = 0;
            var xR = new List<float>();
            var xL = new List<float>();
            for (var i = 0; i < Vertexes.Count - 1; i++)
            {
                k = i < Vertexes.Count ? i + 1 : 1;
                if (Check(i, k, y))
                {
                    var x = -((y * (Vertexes[i].X - Vertexes[k].X))
                                - Vertexes[i].X * Vertexes[k].Y + Vertexes[k].X * Vertexes[i].Y)
                            / (Vertexes[k].Y - Vertexes[i].Y);
                    if (Vertexes[k].Y - Vertexes[i].Y > 0)
                        xR.Add(x);
                    else
                        xL.Add(x);
                }
            }

            if (Check(k, 0, y))
            {
                var x = -(y * (Vertexes[k].X - Vertexes[0].X)
                            - Vertexes[k].X * Vertexes[0].Y + Vertexes[0].X * Vertexes[k].Y)
                        / (Vertexes[0].Y - Vertexes[k].Y);
                if (Vertexes[0].Y - Vertexes[k].Y > 0)
                    xR.Add(x);
                else
                    xL.Add(x);
            }

            return new[] { xL, xR };
        }

        /// <summary>
        /// Проверка пересечения отрезка с прямой У.
        /// </summary>
        /// <returns>True - в случае успеха.</returns>
        private bool Check(int i, int k, int y) =>
            (Vertexes[i].Y < y && Vertexes[k].Y >= y) || (Vertexes[i].Y >= y && Vertexes[k].Y < y);

        /// <summary>
        /// Добавление координаты по оси Х, при пересечении отрезка с прямой У.
        /// </summary>
        private List<float> CheckIntersection(List<float> xs, int i, int k, int y)
        {
            if (Check(i, k, y))
            {
                var x = -((y * (Vertexes[i].X - Vertexes[k].X)) - Vertexes[i].X * Vertexes[k].Y +
                          Vertexes[k].X * Vertexes[i].Y)
                        / (Vertexes[k].Y - Vertexes[i].Y);
                xs.Add(x);
            }

            return xs;
        }

        /// <summary>
        /// Рисование ребер у фигуры (используется только для функций).
        /// </summary>
        public void PaintingLineInFigure()
        {
            for (var i = 0; i < Vertexes.Count - 1; i++)
                Graphics.DrawLine(new Pen(Color, DrawPen.Width), Vertexes[i].ToPoint(), Vertexes[i + 1].ToPoint());
        }

        #endregion

        /// <summary>
        /// Поиск мин/макс X.
        /// </summary>
        /// <returns>Массив из 2 элементов (1 - мин, 2 - макс).</returns>
        private float[] SearchXMinAndMax()
        {
            var min = Vertexes[0].X;
            var max = 0.0f;
            foreach (var t in Vertexes)
            {
                min = t.X < min ? t.X : min;
                max = t.X > max ? t.X : max;
            }

            return new[] { min, max };
        }

        /// <summary>
        /// Поиск мин/макс Y.
        /// </summary>
        /// <returns>Массив из 2 элементов (1 - мин, 2 - макс).</returns>
        public float[] SearchYMinAndMax()
        {
            if (Vertexes.Count == 0)
                return new float[] { 0, 0, 0 };

            var min = Vertexes[0].Y;
            var max = Vertexes[0].Y;
            var j = 0;
            for (var i = 0; i < Vertexes.Count; i++)
            {
                var item = Vertexes[i];
                min = Vertexes[i].Y < min ? Vertexes[i].Y : min;

                if (item.Y > max)
                {
                    max = item.Y;
                    j = i;
                }
            }

            min = min < 0 ? 0 : min;
            max = max > _height ? _height : max;
            return new[] { min, max, j };
        }

        #region Геометрические преобразования

        /// <summary>
        /// Лежат ли координаты <paramref name="mx"/> и <paramref name="my"/> в фигуре.
        /// </summary>
        /// <param name="mx">Координата по оси Х.</param>
        /// <param name="my">Координата по оси У.</param>
        /// <returns>True - в случае успеха.</returns>
        public bool ThisFigure(int mx, int my)
        {
            var m = 0;
            for (var i = 0; i <= Vertexes.Count - 1; i++)
            {
                var k = i < Vertexes.Count - 1 ? i + 1 : 0;
                var pi = Vertexes[i];
                var pk = Vertexes[k];
                if ((pi.Y < my) & (pk.Y >= my) | (pi.Y >= my) & (pk.Y < my)
                    && (my - pi.Y) * (pk.X - pi.X) / (pk.Y - pi.Y) + pi.X < mx)
                    m++;
            }

            return m % 2 == 1;
        }

        /// <summary>
        /// Перемещение фигуры на <paramref name="dx"/> по оси Х и <paramref name="dy"/> по оси У.
        /// </summary>
        /// <param name="dx">Расстояние на которое должно произойти перемещение по оси Х.</param>
        /// <param name="dy">Расстояние на которое должно произойти перемещение по оси У.</param>
        public void Move(int dx, int dy)
        {
            for (var i = 0; i < Vertexes.Count; i++)
            {
                var buffer = new Vertex(Vertexes[i].X + dx, Vertexes[i].Y + dy);
                Vertexes[i] = buffer;
            }

            if (!IsHaveTmo)
                Fill();
        }

        /// <summary>
        /// Перемещение к центру и в нужно местоположение.
        /// </summary>
        private void ToAndFromCenter(bool start, Vertex e)
        {
            if (start)
            {
                float[,] toCenter =
                {
                    { 1, 0, 0 },
                    { 0, 1, 0 },
                    { -e.X, -e.Y, 1 }
                };
                for (var i = 0; i < Vertexes.Count; i++)
                    Vertexes[i] = Matrix_1x3_3x3(Vertexes[i], toCenter);
            }
            else
            {
                float[,] fromCenter =
                {
                    { 1, 0, 0 },
                    { 0, 1, 0 },
                    { e.X, e.Y, 1 }
                };
                for (var i = 0; i < Vertexes.Count; i++)
                    Vertexes[i] = Matrix_1x3_3x3(Vertexes[i], fromCenter);
            }
        }

        /// <summary>
        /// Нахождение центра фигуры.
        /// </summary>
        /// <returns>Вершина с координатами центра фигуры.</returns>
        private Vertex CenterFigure()
        {
            var e = new Vertex();
            var arrayY = SearchYMinAndMax();
            var arrayX = SearchXMinAndMax();
            e.X = (arrayX[0] + arrayX[1]) / 2;
            e.Y = (arrayY[0] + arrayY[1]) / 2;
            return e;
        }

        /// <summary>
        /// Отражение.
        /// </summary>
        public void Mirror(MouseEventArgs em, bool centerFigure)
        {
            var matrix = new float[,]
            {
                {1, 0, 0},
                {0, -1, 0},
                {0, 0, 1}
            };

            var e = centerFigure
                ? CenterFigure()
                : new Vertex() { Y = em.Y };

            ToAndFromCenter(true, e);

            for (var i = 0; i < Vertexes.Count; i++)
                Vertexes[i] = Matrix_1x3_3x3(Vertexes[i], matrix);

            ToAndFromCenter(false, e);
            Fill();
        }

        private int _updateAlpha;

        /// <summary>
        /// Поворот относительно заданного центра.
        /// </summary>
        /// <param name="mouse">Объект события мышки.</param>
        /// <param name="angleBox">Объект для отображения угла поворота.</param>
        public void Rotation(MouseEventArgs mouseArg, TextBox angleBox)
        {
            float alpha = 0;
            if (mouseArg.Delta > 0)
            {
                alpha += 0.0175f;
                _updateAlpha++;
            }
            else
            {
                alpha -= 0.0175f;
                _updateAlpha--;
            }

            angleBox.Text = _updateAlpha.ToString();
            var e = new Vertex(mouseArg.X, mouseArg.Y);
            ToAndFromCenter(true, e);

            float[,] matrixRotation =
            {
                { (float)Math.Cos(alpha), (float)Math.Sin(alpha), 0.0f },
                { -(float)Math.Sin(alpha), (float)Math.Cos(alpha), 0.0f },
                { 0.0f, 0.0f, 1.0f }
            };
            for (var i = 0; i < Vertexes.Count; i++)
                Vertexes[i] = Matrix_1x3_3x3(Vertexes[i], matrixRotation);

            ToAndFromCenter(false, e);
            Fill();
        }

        /// <summary>
        /// Умножение матрицы 1х3 на 3х3.
        /// </summary>
        private static Vertex Matrix_1x3_3x3(Vertex point, float[,] matrix3X3) =>
            new(
                point.X * matrix3X3[0, 0] + point.Y * matrix3X3[1, 0] + point.Thirst * matrix3X3[2, 0],
                point.X * matrix3X3[0, 1] + point.Y * matrix3X3[1, 1] + point.Thirst * matrix3X3[2, 1],
                point.X * matrix3X3[0, 2] + point.Y * matrix3X3[1, 2] + point.Thirst * matrix3X3[2, 2]
            );

        #endregion
    }
}
