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

    pb.Image = bmp;
};

form.KeyDown += (sender, e) => {
    if (e.KeyCode == Keys.Escape)
        Application.Exit();
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

