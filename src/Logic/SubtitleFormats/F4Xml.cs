﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;

namespace Nikse.SubtitleEdit.Logic.SubtitleFormats
{
    class F4Xml : F4Text
    {
        public override string Extension
        {
            get { return ".xml"; }
        }

        public override string Name
        {
            get { return "F4 Xml"; }
        }

        public override bool HasLineNumber
        {
            get { return false; }
        }

        public override bool IsTimeBased
        {
            get { return true; }
        }

        public override bool IsMine(List<string> lines, string fileName)
        {
            if (!fileName.ToLower().EndsWith(Extension))
                return false;

            var subtitle = new Subtitle();
            LoadSubtitle(subtitle, lines, fileName);
            return subtitle.Paragraphs.Count > _errorCount;
        }

        public override string ToText(Subtitle subtitle, string title)
        {
            var xml = new XmlDocument();
            var template = @"<?xml version='1.0' encoding='utf-8' standalone='no'?>
<transcript>
    <head mediafile=''/>
    <content content=''/>
    <style>
    </style>
</transcript>".Replace("'", "\"");
            xml.LoadXml(template);
            xml.DocumentElement.SelectSingleNode("content").Attributes["content"].Value = ToF4Text(subtitle, title);

            MemoryStream ms = new MemoryStream();
            XmlTextWriter writer = new XmlTextWriter(ms, Encoding.UTF8);
            writer.Formatting = Formatting.Indented;
            xml.Save(writer);
            return Encoding.UTF8.GetString(ms.ToArray()).Trim();
        }

        public override void LoadSubtitle(Subtitle subtitle, List<string> lines, string fileName)
        {
            StringBuilder sb = new StringBuilder();
            foreach (string line in lines)
                sb.AppendLine(line);

            var doc = new XmlDocument();
            doc.LoadXml(sb.ToString());
            string text = doc.DocumentElement.SelectSingleNode("content").Attributes["content"].Value;           
            LoadF4TextSubtitle(subtitle, text);
        }
    }
}