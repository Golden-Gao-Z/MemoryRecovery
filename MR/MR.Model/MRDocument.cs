using System.Runtime.CompilerServices;

namespace MR.Model
{
    public class MRDocument
    {
        /// <summary>
        /// content level, like theme in word files, doc, docx.
        /// </summary>
        public int Level { get; set; }
        public string Style { get; set; }
        public string Title { get; set; }
        /// <summary>
        /// could be text, pic, audio, video, or some others.
        /// if MRDocumentType is leaf, Children should be empty array, not null.
        /// </summary>
        public object Content { get; set; }
        public MRDocument[] Children { get; set; }
        public MRDocument Parent { get; set; }
        public MRDocument Root { get; set; }
        public MRDocument Previous { get; set; }
        public MRDocument Next { get; set; }
        public MRDocumentType MRDocumentType
        {
            get
            {
                if (this.Level == 0)
                    return MRDocumentType.Root;

                if (!int.TryParse(this.Style, out _))
                    return MRDocumentType.Leaf;

                return MRDocumentType.MiddleNode;


            }
        }

        public MRDocument() { }

        public bool CheckDocument()
        {
            // check doc structure:
            // 1 only one level0 node can be exists.
            // 2 leaf can not contains nodes.
            // 3 ... ...
            return true;
        }

    }
    public enum MRDocumentType
    {
        Root,// as its name means
        MiddleNode, // node that holds some leafsContainers, can also holds leafs. But its not Root
        LeafsContainer,// node that holds some leaf nodes. 
        Leaf
    }
}
