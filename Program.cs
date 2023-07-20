#pragma warning disable
ApplicationConfiguration.Initialize();

var form = new Form();
form.WindowState = FormWindowState.Maximized;
form.FormBorderStyle = FormBorderStyle.None;
form.Dock = DockStyle.Fill;

PictureBox pb = new PictureBox();
Graphics g = null;
Bitmap bmp = null;

List<Entity> all_entities = new List<Entity>();
//! Entities
FpsCounter fps = new FpsCounter();
all_entities.Add(fps);

Ken k = new Ken();
all_entities.Add(k);
//! Add to list

pb.Dock = DockStyle.Fill;
form.Controls.Add(pb);

int ticks = 17;
fps.Ticks = ticks;
System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer() {
    Interval = ticks
};

form.Load += delegate {
    timer.Start();

    bmp = new Bitmap(pb.Width, pb.Height);
    g = Graphics.FromImage(bmp);

    k.ScreenSize = new Size(bmp.Width, bmp.Height);

    pb.Image = bmp;
};

form.KeyDown += (sender, e) => {
    if (e.KeyCode == Keys.Escape)
        Application.Exit();
    
    if (e.KeyCode == Keys.A)
    {
        k.CurrentState = States.Backward;
    }
    
    else if (e.KeyCode == Keys.D)
    {
        k.CurrentState = States.Forward;
    }
    
    else if (e.KeyCode == Keys.W)
    {
        k.CurrentState = States.Jump;
    }

    else
    {
        k.CurrentState = States.Idle;
    }

};

timer.Tick += delegate {
    g.Clear(Color.White); // clear screen

    foreach (var item in all_entities)
        item.Update(g, fps.TimePerFrame);

    foreach (var item in all_entities)
        item.Draw(g);

    pb.Refresh();
};

Application.Run(form);

