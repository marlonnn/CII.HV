using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CII.LAR.Laser;
using CII.LAR.Protocol;
using CII.LAR.Commond;
using CII.LAR.Algorithm;
using CII.LAR.SysClass;
using MathNet.Numerics.LinearAlgebra;

namespace CII.LAR.UI
{
    /// <summary>
    /// Laser alignment
    /// Author: Zhong Wen 2018/10/18
    /// </summary>
    public partial class LaserAlignment : BaseCtrl
    {
        private int index;
        public int Index
        {
            get { return index; }
            set
            {
                if (value != this.index)
                {
                    this.index = value;
                    helper.AlignmentInfo(index, true);
                    this.Invalidate();
                }
            }
        }

        private AlignInfoHelper helper;
        private RichPictureBox richPictureBox;
        public RichPictureBox RichPictureBox
        {
            get { return this.richPictureBox; }
            set { this.richPictureBox = value; }
        }

        public Label LabelInfo
        {
            get { return this.lblInfo; }
        }

        public void ButtonBack(bool isEnable)
        {
            this.btnBack.Enabled = isEnable;
        }

        public void ButtonNext(bool isEnable)
        {
            this.btnNext.Enabled = isEnable;
        }

        public LaserAlignment() :base()
        {
            resources = new ComponentResourceManager(typeof(LaserAlignment));
            this.ShowIndex = 6;
            InitializeComponent();
            helper = new AlignInfoHelper(this);
            this.Load += LaserAlignment_Load;
            Index = -2;
            this.KeyDown += LaserAlignment_KeyDown;
        }

        private void LaserAlignment_KeyDown(object sender, KeyEventArgs e)
        {
            if (VideoKeyDownHandler != null)
            {
                VideoKeyDownHandler(e);
            }
        }

        private void LaserAlignment_Load(object sender, System.EventArgs e)
        {
            this.lblInfo.Text = Res.LaserAlignment.StrPreSet0;
            Program.EntryForm.LaserType = LaserType.Alignment;
            //if (Program.ExpManager.MachineStatus == MachineStatus.Simulate)
            //    this.pictureBox.Invalidate();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (Index != 7)
            {
                Index++;
                helper.AlignmentInfo(index, index < 0);
                if (Index == -1)
                {
                    this.btnNext.Text = Res.LaserAlignment.StrAlignLaser;
                }
                else if (Index == 7)
                {
                    this.btnNext.Text = Res.LaserAlignment.StrSave;
                    //计算平均转换矩阵
                    Coordinate.GetCoordinate().CalculateOtherMatix();
                    Coordinate.GetCoordinate().GetFinalMatrix();
                    var v = Program.SysConfig.LaserConfig.FinalMatrix;
                    Console.WriteLine(" final matrix: " + v.ToString());
                    Console.WriteLine(" final matrix Rank: " + v.Rank());
                    string matrixJsonString = JsonFile.GetJsonTextFromConfig<Matrix<double>>(v);
                    JsonFile.WriteMatrixConfigToLocal(matrixJsonString);
                }
                else
                {
                    this.btnNext.Text = Res.LaserAlignment.StrNext;
                }
                if (Index > -1 && Index < 7)
                {
                    if (Index >= 3 && Index <= 6)
                    {
                        if (Index == 3)
                        {
                            Coordinate.GetCoordinate().CalculateFirstMatrix();
                            var v = Coordinate.GetCoordinate().FistMatrix;
                            Console.WriteLine(" first matrix: " + v.ToString());
                            Console.WriteLine(" first matrix Rank: " + v.Rank());
                        }
                        Coordinate.GetCoordinate().CreatePresetMotorPoint(Index, this.RichPictureBox);
                    }
                    SendAlignmentMotorPoint();
                    AlignLaser laser = Program.EntryForm.Laser as AlignLaser;
                    if (laser != null)
                    {
                        laser.Index = Index;
                    }
                    //this.pictureBox.ZoomFit();
                }
            }
            else
            {
                this.Visible = false;
                this.Enabled = false;
                this.RichPictureBox.ZoomFit();
            }
        }

        private void SendAlignmentMotorPoint()
        {
            if (Index >= 0 && Index <= 6)
            {
                //if (Index > 0)
                    //Coordinate.GetCoordinate().LastPoint = Coordinate.GetCoordinate().MotorPoints[Index - 1];
                Coordinate.GetCoordinate().ThisPoint = Coordinate.GetCoordinate().MotorPoints[Index];
                Coordinate.GetCoordinate().SendAlignmentMotorPoint();
            }
        }

        protected override void RefreshUI()
        {
            base.RefreshUI();

            this.Title = Res.LaserAlignment.StrTitle;
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            if (Index != -2)
            {
                Index--;
                helper.AlignmentInfo(index, index < 0);
                if (Index > -1 && Index < 7)
                {
                    AlignLaser laser = Program.EntryForm.Laser as AlignLaser;
                    if (laser != null)
                    {
                        laser.Index = Index;
                    }
                }
            }
            else
            {
                this.Visible = false;
                this.Enabled = false;
                Program.EntryForm.ShowBaseCtrl(true, 0);
            }
        }

        public class AlignInfoHelper
        {
            private LaserAlignment laserAlignment;

            public AlignInfoHelper(LaserAlignment laserAlignment)
            {
                this.laserAlignment = laserAlignment;
            }
            public void AlignmentInfo(int index, bool isTitle)
            {
                switch (index)
                {
                    case -2:
                        laserAlignment.Title = Res.LaserAlignment.StrTitle;
                        laserAlignment.LabelInfo.Text = Res.LaserAlignment.StrPreSet0;
                        break;
                    case -1:
                        laserAlignment.Title = Res.LaserAlignment.StrTitle;
                        laserAlignment.LabelInfo.Text = Res.LaserAlignment.StrPreSet1;
                        break;
                    case 0:
                        laserAlignment.Title = Res.LaserAlignment.StrStepTitleOne;
                        laserAlignment.LabelInfo.Text = Res.LaserAlignment.StrStepOne;
                        break;
                    case 1:
                        laserAlignment.Title = Res.LaserAlignment.StrStepTitleTwo;
                        laserAlignment.LabelInfo.Text = Res.LaserAlignment.StrStepTwo;
                        break;
                    case 2:
                        laserAlignment.Title = Res.LaserAlignment.StrStepTitleThree;
                        laserAlignment.LabelInfo.Text = Res.LaserAlignment.StrStepThree;
                        break;
                    case 3:
                        laserAlignment.Title = Res.LaserAlignment.StrStepTitleFour;
                        laserAlignment.LabelInfo.Text = Res.LaserAlignment.StrStepFour;
                        break;
                    case 4:
                        laserAlignment.Title = Res.LaserAlignment.StrStepTitleFive;
                        laserAlignment.LabelInfo.Text = Res.LaserAlignment.StrStepFive;
                        break;
                    case 5:
                        laserAlignment.Title = Res.LaserAlignment.StrStepTitleSix;
                        laserAlignment.LabelInfo.Text = Res.LaserAlignment.StrStepSix;
                        break;
                    case 6:
                        laserAlignment.Title = Res.LaserAlignment.StrStepTitleSeven;
                        laserAlignment.LabelInfo.Text = Res.LaserAlignment.StrStepSeven;
                        break;
                    case 7:
                        laserAlignment.Title = Res.LaserAlignment.StrStepTitleComplete;
                        laserAlignment.LabelInfo.Text = Res.LaserAlignment.StrStepComplete;
                        break;
                }
            }

        }

        private void LaserAlignment_VisibleChanged(object sender, EventArgs e)
        {
            Index = -2;
            if (this.Visible)
            {
                AlignLaser laser = Program.EntryForm.Laser as AlignLaser;
                if (laser != null)
                {
                    laser.IsAlign = this.Visible;
                }
                ButtonBack(true);
                ButtonNext(true);
                Program.EntryForm.LaserType = LaserType.Alignment;
            }
            else
            {
                Program.EntryForm.LaserType = LaserType.SaturnFixed;
            }
        }

        public override void InitializeLocation(Size size)
        {
            this.Location = new Point(size.Width - this.Width - 20, size.Height - this.Height - 20);
        }
    }
}
