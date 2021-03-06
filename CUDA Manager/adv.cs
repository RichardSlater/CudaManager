﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CUDA_Manager
{
    public partial class adv : Form
    {
        private Form1 parent;
        public adv(Form1 parent)
        {
            this.parent = parent;
            InitializeComponent();
        }

        private void buOkay_Click(object sender, EventArgs e)
        {
            if (chkAutoStart.Checked)
            {
                parent.AutoStart = true;
            }
            else
                parent.AutoStart = false;

            if (chkHaltRet.Checked)
            {
                parent.HaltRet = true;
            }
            else
                parent.HaltRet = false;


            if (chkIdle.Checked)
                parent.idlestart = true;
            else
                parent.idlestart = false;

            parent.idleminer = cbIdleMine.Text;
            parent.idletimer = (int)numIdle.Value * 60;

            if (numAct.Value > 80)
            {
                DialogResult dialogResult = MessageBox.Show("Please be cautious when altering default temperature settings.\r\n\r\nIt's highly advised against raising Protective Cooling's limit above 80C.\r\nInstead, please check your fans for dust.\r\n\r\nDo you want to continue?", "Override Defaults?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    parent.unsafetmp = (int)numAct.Value;
                    parent.shutdowntmp = (int)numShut.Value;
                    this.Close();
                }
            }
            else
            {
                parent.unsafetmp = (int)numAct.Value;
                parent.shutdowntmp = (int)numShut.Value;
                this.Close();
            }
        }

        private void buReset_Click(object sender, EventArgs e)
        {
            numAct.Value = 80;
            numShut.Value = 100;
            chkAutoStart.Checked = false;
            chkHaltRet.Checked = false;
            chkIdle.Checked = false;

        }

        private void adv_Load(object sender, EventArgs e)
        {
            numAct.Value = parent.unsafetmp;
            numShut.Value = parent.shutdowntmp;

            if (parent.AutoStart)
            {
                chkAutoStart.Checked = true;
            }

            if (parent.HaltRet)
            {
                chkHaltRet.Checked = true;
            }

            if (parent.idletimer >= 60)
                numIdle.Value = parent.idletimer / 60;

            if (parent.idlestart)
                chkIdle.Checked = true;

            foreach (string miner in parent.miners)
            {
                cbIdleMine.Items.Add(miner);
            }

            if (cbIdleMine.Items.Count > 0)
                cbIdleMine.SelectedIndex = 0;

            if (parent.idleminer != "" && parent.idleminer != null)
            {
                if (cbIdleMine.Items.Contains(parent.idleminer))
                    cbIdleMine.SelectedItem = parent.idleminer;
            }
                
        }
    }
}
