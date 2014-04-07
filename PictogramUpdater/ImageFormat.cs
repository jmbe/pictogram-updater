using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PictogramUpdater {

    public class ImageFormatProvider {
        public IList<ImageFormat> Formats {
            get {
                IList<ImageFormat> result = new List<ImageFormat>();

                result.Add(new ImageFormat("wmf", "WMF (" + TextResources.pictogramManager + ")"));
                result.Add(new ImageFormat("png", "PNG"));
                result.Add(ImageFormat.JPG);
                result.Add(ImageFormat.SVG);

                return result;
            }
        }
    }

    public class ImageFormat {

        public static readonly ImageFormat SVG = new ImageFormat("svg", "SVG");
        public static readonly ImageFormat JPG = new ImageFormat("jpg", "JPEG");

        public ImageFormat(string extension, string display) {
            Extension = extension;
            Display = display;
        }

        /* Lower case extension; for filenames */
        public string Extension {
            get;
            private set;
        }

        public string Display {
            get;
            private set;
        }

        /* First letter capitalized; for directory names and ini filename. */
        public string CapitalizedExtension {
            get {
                return char.ToUpper(Extension[0]) + Extension.Substring(1);
            }
        }

        /* Extension in all caps letters; for entries in ini file. */
        public string AllCapsExtension {
            get {
                return Extension.ToUpper();
            }
        }

        public bool IsVectorFormat {
            get {
                return "wmf".Equals(Extension.ToLower()) || IsSvg;
            }
        }

        public bool IsSvg {
            get {
                return "svg".Equals(Extension.ToLower());
            }
        }

        public override string ToString() {
            return Display;
        }
    }
}

