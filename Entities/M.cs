namespace CourseWork90
{
    /// <summary>
    /// Специальная координата для корректной работы ТМО.
    /// </summary>
    public class M
    {
        /// <summary>
        /// Координата по оси Х.
        /// </summary>
        public float X { get; }

        /// <summary>
        /// Вес.
        /// </summary>
        public int Dq { get; }

        public M(float x, int dQ)
        {
            X = x;
            Dq = dQ;
        }
    }
}
