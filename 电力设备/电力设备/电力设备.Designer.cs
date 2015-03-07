namespace DrawElements
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.打开ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.保存ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.另存为ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.退出ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.绘制ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.母线ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.线路ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.开关ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.刀闸ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.电容ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.接地线ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.发电机ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.二绕阻变压器ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.三绕阻变压器ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.操作ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.移动ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.删除ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.旋转ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.放大ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.缩小ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.连线ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.着色ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.刷新ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.清空ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.建立连接ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.断开服务器ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.加点ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.开关ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // 文件ToolStripMenuItem
            // 
            this.文件ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.打开ToolStripMenuItem,
            this.保存ToolStripMenuItem,
            this.另存为ToolStripMenuItem,
            this.退出ToolStripMenuItem});
            this.文件ToolStripMenuItem.Name = "文件ToolStripMenuItem";
            this.文件ToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.文件ToolStripMenuItem.Text = "文件";
            // 
            // 打开ToolStripMenuItem
            // 
            this.打开ToolStripMenuItem.Name = "打开ToolStripMenuItem";
            this.打开ToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            this.打开ToolStripMenuItem.Text = "打开";
            this.打开ToolStripMenuItem.Click += new System.EventHandler(this.menu_Click);
            // 
            // 保存ToolStripMenuItem
            // 
            this.保存ToolStripMenuItem.Name = "保存ToolStripMenuItem";
            this.保存ToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            this.保存ToolStripMenuItem.Text = "保存";
            this.保存ToolStripMenuItem.Click += new System.EventHandler(this.menu_Click);
            // 
            // 另存为ToolStripMenuItem
            // 
            this.另存为ToolStripMenuItem.Name = "另存为ToolStripMenuItem";
            this.另存为ToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            this.另存为ToolStripMenuItem.Text = "另存为";
            this.另存为ToolStripMenuItem.Click += new System.EventHandler(this.menu_Click);
            // 
            // 退出ToolStripMenuItem
            // 
            this.退出ToolStripMenuItem.Name = "退出ToolStripMenuItem";
            this.退出ToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            this.退出ToolStripMenuItem.Text = "退出";
            this.退出ToolStripMenuItem.Click += new System.EventHandler(this.menu_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.文件ToolStripMenuItem,
            this.绘制ToolStripMenuItem,
            this.操作ToolStripMenuItem,
            this.刷新ToolStripMenuItem,
            this.清空ToolStripMenuItem1,
            this.建立连接ToolStripMenuItem,
            this.断开服务器ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(797, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 绘制ToolStripMenuItem
            // 
            this.绘制ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.母线ToolStripMenuItem,
            this.线路ToolStripMenuItem,
            this.开关ToolStripMenuItem,
            this.刀闸ToolStripMenuItem,
            this.电容ToolStripMenuItem,
            this.接地线ToolStripMenuItem,
            this.发电机ToolStripMenuItem,
            this.二绕阻变压器ToolStripMenuItem,
            this.三绕阻变压器ToolStripMenuItem});
            this.绘制ToolStripMenuItem.Name = "绘制ToolStripMenuItem";
            this.绘制ToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.绘制ToolStripMenuItem.Text = "绘制";
            // 
            // 母线ToolStripMenuItem
            // 
            this.母线ToolStripMenuItem.Name = "母线ToolStripMenuItem";
            this.母线ToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.母线ToolStripMenuItem.Text = "母线";
            this.母线ToolStripMenuItem.Click += new System.EventHandler(this.menu_Click);
            // 
            // 线路ToolStripMenuItem
            // 
            this.线路ToolStripMenuItem.Name = "线路ToolStripMenuItem";
            this.线路ToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.线路ToolStripMenuItem.Text = "线路";
            this.线路ToolStripMenuItem.Click += new System.EventHandler(this.menu_Click);
            // 
            // 开关ToolStripMenuItem
            // 
            this.开关ToolStripMenuItem.Name = "开关ToolStripMenuItem";
            this.开关ToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.开关ToolStripMenuItem.Text = "开关";
            this.开关ToolStripMenuItem.Click += new System.EventHandler(this.menu_Click);
            // 
            // 刀闸ToolStripMenuItem
            // 
            this.刀闸ToolStripMenuItem.Name = "刀闸ToolStripMenuItem";
            this.刀闸ToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.刀闸ToolStripMenuItem.Text = "刀闸";
            this.刀闸ToolStripMenuItem.Click += new System.EventHandler(this.menu_Click);
            // 
            // 电容ToolStripMenuItem
            // 
            this.电容ToolStripMenuItem.Name = "电容ToolStripMenuItem";
            this.电容ToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.电容ToolStripMenuItem.Text = "电容";
            this.电容ToolStripMenuItem.Click += new System.EventHandler(this.menu_Click);
            // 
            // 接地线ToolStripMenuItem
            // 
            this.接地线ToolStripMenuItem.Name = "接地线ToolStripMenuItem";
            this.接地线ToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.接地线ToolStripMenuItem.Text = "接地线";
            this.接地线ToolStripMenuItem.Click += new System.EventHandler(this.menu_Click);
            // 
            // 发电机ToolStripMenuItem
            // 
            this.发电机ToolStripMenuItem.Name = "发电机ToolStripMenuItem";
            this.发电机ToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.发电机ToolStripMenuItem.Text = "发电机";
            this.发电机ToolStripMenuItem.Click += new System.EventHandler(this.menu_Click);
            // 
            // 二绕阻变压器ToolStripMenuItem
            // 
            this.二绕阻变压器ToolStripMenuItem.Name = "二绕阻变压器ToolStripMenuItem";
            this.二绕阻变压器ToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.二绕阻变压器ToolStripMenuItem.Text = "二绕阻变压器";
            this.二绕阻变压器ToolStripMenuItem.Click += new System.EventHandler(this.menu_Click);
            // 
            // 三绕阻变压器ToolStripMenuItem
            // 
            this.三绕阻变压器ToolStripMenuItem.Name = "三绕阻变压器ToolStripMenuItem";
            this.三绕阻变压器ToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.三绕阻变压器ToolStripMenuItem.Text = "三绕阻变压器";
            this.三绕阻变压器ToolStripMenuItem.Click += new System.EventHandler(this.menu_Click);
            // 
            // 操作ToolStripMenuItem
            // 
            this.操作ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.移动ToolStripMenuItem,
            this.删除ToolStripMenuItem,
            this.旋转ToolStripMenuItem,
            this.放大ToolStripMenuItem,
            this.缩小ToolStripMenuItem,
            this.连线ToolStripMenuItem1,
            this.着色ToolStripMenuItem1});
            this.操作ToolStripMenuItem.Enabled = false;
            this.操作ToolStripMenuItem.Name = "操作ToolStripMenuItem";
            this.操作ToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.操作ToolStripMenuItem.Text = "操作";
            // 
            // 移动ToolStripMenuItem
            // 
            this.移动ToolStripMenuItem.Name = "移动ToolStripMenuItem";
            this.移动ToolStripMenuItem.Size = new System.Drawing.Size(98, 22);
            this.移动ToolStripMenuItem.Text = "移动";
            this.移动ToolStripMenuItem.Click += new System.EventHandler(this.menu_Click);
            // 
            // 删除ToolStripMenuItem
            // 
            this.删除ToolStripMenuItem.Name = "删除ToolStripMenuItem";
            this.删除ToolStripMenuItem.ShortcutKeyDisplayString = "";
            this.删除ToolStripMenuItem.Size = new System.Drawing.Size(98, 22);
            this.删除ToolStripMenuItem.Text = "删除";
            this.删除ToolStripMenuItem.Click += new System.EventHandler(this.menu_Click);
            // 
            // 旋转ToolStripMenuItem
            // 
            this.旋转ToolStripMenuItem.Name = "旋转ToolStripMenuItem";
            this.旋转ToolStripMenuItem.Size = new System.Drawing.Size(98, 22);
            this.旋转ToolStripMenuItem.Text = "旋转";
            this.旋转ToolStripMenuItem.Click += new System.EventHandler(this.menu_Click);
            // 
            // 放大ToolStripMenuItem
            // 
            this.放大ToolStripMenuItem.Name = "放大ToolStripMenuItem";
            this.放大ToolStripMenuItem.Size = new System.Drawing.Size(98, 22);
            this.放大ToolStripMenuItem.Text = "放大";
            this.放大ToolStripMenuItem.Click += new System.EventHandler(this.menu_Click);
            // 
            // 缩小ToolStripMenuItem
            // 
            this.缩小ToolStripMenuItem.Name = "缩小ToolStripMenuItem";
            this.缩小ToolStripMenuItem.Size = new System.Drawing.Size(98, 22);
            this.缩小ToolStripMenuItem.Text = "缩小";
            this.缩小ToolStripMenuItem.Click += new System.EventHandler(this.menu_Click);
            // 
            // 连线ToolStripMenuItem1
            // 
            this.连线ToolStripMenuItem1.Name = "连线ToolStripMenuItem1";
            this.连线ToolStripMenuItem1.Size = new System.Drawing.Size(98, 22);
            this.连线ToolStripMenuItem1.Text = "连线";
            this.连线ToolStripMenuItem1.Click += new System.EventHandler(this.menu_Click);
            // 
            // 着色ToolStripMenuItem1
            // 
            this.着色ToolStripMenuItem1.Name = "着色ToolStripMenuItem1";
            this.着色ToolStripMenuItem1.Size = new System.Drawing.Size(98, 22);
            this.着色ToolStripMenuItem1.Text = "着色";
            this.着色ToolStripMenuItem1.Click += new System.EventHandler(this.menu_Click);
            // 
            // 刷新ToolStripMenuItem
            // 
            this.刷新ToolStripMenuItem.Name = "刷新ToolStripMenuItem";
            this.刷新ToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.刷新ToolStripMenuItem.Text = "刷新";
            this.刷新ToolStripMenuItem.Click += new System.EventHandler(this.menu_Click);
            // 
            // 清空ToolStripMenuItem1
            // 
            this.清空ToolStripMenuItem1.Name = "清空ToolStripMenuItem1";
            this.清空ToolStripMenuItem1.Size = new System.Drawing.Size(43, 20);
            this.清空ToolStripMenuItem1.Text = "清空";
            this.清空ToolStripMenuItem1.Click += new System.EventHandler(this.menu_Click);
            // 
            // 建立连接ToolStripMenuItem
            // 
            this.建立连接ToolStripMenuItem.Name = "建立连接ToolStripMenuItem";
            this.建立连接ToolStripMenuItem.Size = new System.Drawing.Size(67, 20);
            this.建立连接ToolStripMenuItem.Text = "建立连接";
            this.建立连接ToolStripMenuItem.Click += new System.EventHandler(this.menu_Click);
            // 
            // 断开服务器ToolStripMenuItem
            // 
            this.断开服务器ToolStripMenuItem.Name = "断开服务器ToolStripMenuItem";
            this.断开服务器ToolStripMenuItem.Size = new System.Drawing.Size(79, 20);
            this.断开服务器ToolStripMenuItem.Text = "断开服务器";
            this.断开服务器ToolStripMenuItem.Click += new System.EventHandler(this.menu_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.加点ToolStripMenuItem,
            this.开关ToolStripMenuItem1});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(104, 48);
            // 
            // 加点ToolStripMenuItem
            // 
            this.加点ToolStripMenuItem.Name = "加点ToolStripMenuItem";
            this.加点ToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.加点ToolStripMenuItem.Text = "加点";
            this.加点ToolStripMenuItem.Click += new System.EventHandler(this.menu_Click);
            // 
            // 开关ToolStripMenuItem1
            // 
            this.开关ToolStripMenuItem1.Name = "开关ToolStripMenuItem1";
            this.开关ToolStripMenuItem1.Size = new System.Drawing.Size(103, 22);
            this.开关ToolStripMenuItem1.Text = "开/关";
            this.开关ToolStripMenuItem1.Visible = false;
            this.开关ToolStripMenuItem1.Click += new System.EventHandler(this.menu_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(797, 467);
            this.ContextMenuStrip = this.contextMenuStrip1;
            this.Controls.Add(this.menuStrip1);
            this.Name = "Form1";
            this.Text = "电力设备";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseUp);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStripMenuItem 文件ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 打开ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 保存ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 另存为ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 退出ToolStripMenuItem;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 绘制ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 母线ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 线路ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 开关ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 刀闸ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 电容ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 接地线ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 发电机ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 二绕阻变压器ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 三绕阻变压器ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 操作ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 移动ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 删除ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 旋转ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 刷新ToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 开关ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 加点ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 放大ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 缩小ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 连线ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 着色ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 清空ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 建立连接ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 断开服务器ToolStripMenuItem;
    }
}

