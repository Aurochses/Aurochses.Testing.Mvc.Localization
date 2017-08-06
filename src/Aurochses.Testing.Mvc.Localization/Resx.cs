using System.Collections.Generic;
using System.Xml.Serialization;

namespace Aurochses.Testing.Mvc.Localization
{
    /// <summary>
    /// Class Resx.
    /// </summary>
    [XmlRoot("root")]
    public class Resx
    {
        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        /// <value>
        /// The data.
        /// </value>
        [XmlElement("data")]
        public List<DataNode> Data { get; set; }

        /// <summary>
        /// Class DataNode.
        /// </summary>
        public class DataNode
        {
            /// <summary>
            /// Gets or sets the name.
            /// </summary>
            /// <value>
            /// The name.
            /// </value>
            [XmlAttribute("name")]
            public string Name { get; set; }

            /// <summary>
            /// Gets or sets the value.
            /// </summary>
            /// <value>
            /// The value.
            /// </value>
            [XmlElement("value")]
            public string Value { get; set; }
        }
    }
}