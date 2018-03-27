using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CII.LAR.ExpClass
{
    public class ReportPictureItem : ReportItemBase
    {
        private Image picture;

        public Image Picture
        {
            get
            {
                return this.picture;
            }
            set
            {
                this.picture = value;
            }
        }

        private Point oldImageLocation;
        public Point OldImageLocation
        {
            get
            {
                return this.oldImageLocation;
            }
            set
            {
                this.oldImageLocation = value;
            }
        }

        private Size oldImageSize;

        public Size OldImageSize
        {
            get
            {
                return this.oldImageSize;
            }
            set
            {
                this.oldImageSize = value;
            }
        }

        private Size newImageSize;
        public Size NewImageSize
        {
            get
            {
                return this.newImageSize;
            }
            set
            {
                this.newImageSize = value;
            }
        }
    }
}
