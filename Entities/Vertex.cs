namespace CourseWork90
{
    /// <summary>
    /// Вершина.
    /// </summary>
    public class Vertex
    {
        /// <summary>
        /// Координата по оси Х.
        /// </summary>
        public float X;

        /// <summary>
        /// Координата по оси У.
        /// </summary>
        public float Y;

        /// <summary>
        /// Вспомогательная переменная для вычисления в геометрических преобразованиях.
        /// </summary>
        public readonly float Thirst;

        public Vertex(float x = 0.0f, float y = 0.0f, float thirst = 1.0f)
        {
            X = x;
            Y = y;
            Thirst = thirst;
        }

        public Vertex(float x, float y)
        {
            X = x;
            Y = y;
            Thirst = 1.0f;
        }

        public Point ToPoint() => new((int)X, (int)Y);
    }
}
