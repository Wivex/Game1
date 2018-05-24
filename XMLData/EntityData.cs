using System.Xml;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Intermediate;
using Microsoft.Xna.Framework.Graphics;

namespace XMLData
{
    public abstract class EntityData
    {
        public string Name { get; set; } = "Noname";

        /// <summary>
        /// Generate xml template based on pre-initialized data class
        /// </summary>
        /// <param name="outputPath">example: @"C:\...\File.xml"</param>
        /// <returns></returns>
        public void GenerateTemplate(string outputPath)
        {
            var xmlWriterSettings = new XmlWriterSettings
            {
                Indent = true,
                IndentChars = "\t"
            };

            using (var writer = XmlWriter.Create(outputPath, xmlWriterSettings))
            {
                IntermediateSerializer.Serialize(writer, this, null);
            }
        }
    }
}