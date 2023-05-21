﻿namespace JobBoardPlatform.BLL.Common.Formatter
{
    public class DaysFormatter : ITextFormatter<DateTime>
    {
        private readonly bool isPublished;


        public DaysFormatter(bool isPublished)
        {
            this.isPublished = isPublished;
        }

        public string GetString(DateTime date)
        {
            string publishedAgo = $"0d ago";

            int daysAgo = (DateTime.Now - date).Days;

            if (daysAgo == 0)
            {
                return "new";
            }

            if (isPublished)
            {
                publishedAgo = $"{daysAgo}d ago";
            }

            return publishedAgo;
        }
    }
}