using System;
using System.Collections.Generic;

namespace CoursesManagerLib
{
    // Добавить  сообщения ошибкам
    public class Course : IEquatable<Course>
    {
        private int _intensity;
        private int _duration;
        private int _price;

        private const int MaxPrice = 1000000;

        public readonly string Language;
        public readonly string Level;
        public readonly Format Format;

        public int Intensity
        {
            get { return _intensity; }
            private set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException();
                _intensity = value;
            }
        }

        public int Duration
        {
            get { return _duration; }
            private set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException();
                _duration = value;
            }
        }

        public int Price
        {
            get { return _price; }
            set
            {
                if (value < 0 || value >= MaxPrice)
                    throw new ArgumentOutOfRangeException();
                _price = value;
            }

        }

        public Course(string language, int intensity, string level, Format format, int duration, int price)
        {
            Language = language;
            Intensity = intensity;
            Level = level;
            Format = format;
            Duration = duration;
            Price = price;
        }

        public bool Equals(Course other)
        {
            return other != null && Language == other.Language && Intensity == other.Intensity &&
                   Level == other.Level && Format == other.Format;
        }
        public override string ToString()
        {
            return String.Format("Language  {0}\nLevel   {1}\nDuration  {2}\nFormat   {3}\nPrice   {4}\nIntensity   {5}\r\n", Language, Level, Duration, Format,Price, Intensity);
        }
    }
    
    public enum Format
    {
        Individual,
        Group
    }
}
