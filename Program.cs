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
//! Select person
// Ruyviu k = new Ruyviu();
Joe k = new Joe();

all_entities.Add(k);

Joe j = new Joe();
all_entities.Add(j);

k.Enemy = j;
j.Enemy = k;
//! Add to list

//? Button Handlers
bool isWPressed = false;
bool isAPressed = false;
bool isSPressed = false;
bool isDPressed = false;
bool isJPressed = false;
bool isKPressed = false;
bool isLPressed = false;
bool isUPressed = false;
bool isIPressed = false;
bool isOPressed = false;
//? Button Handlers

pb.Dock = DockStyle.Fill;
form.Controls.Add(pb);

int ticks = 15;
fps.Ticks = ticks;
System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer() {
    Interval = ticks
};

form.Load += delegate {
    timer.Start();

    bmp = new Bitmap(pb.Width, pb.Height);
    g = Graphics.FromImage(bmp);

    k.ScreenSize = new Size(bmp.Width, bmp.Height);
    j.ScreenSize = k.ScreenSize;
    
    pb.Image = bmp;
};

form.KeyDown += (sender, e) => {
    if (e.KeyCode == Keys.Escape)
        Application.Exit();
    
    if (e.KeyCode == Keys.A)
        isAPressed = true;
    if (e.KeyCode == Keys.D)
        isDPressed = true;
    if (e.KeyCode == Keys.W)
        isWPressed = true;
    if (e.KeyCode == Keys.S)
        isSPressed = true;

    if (e.KeyCode == Keys.J)
        isJPressed = true;
    if (e.KeyCode == Keys.K)
        isKPressed = true;
    if (e.KeyCode == Keys.L)
        isLPressed = true;
    if (e.KeyCode == Keys.U)
        isUPressed = true;
    if (e.KeyCode == Keys.I)
        isIPressed = true;
    if (e.KeyCode == Keys.O)
        isOPressed = true;
    

    if (isWPressed && isAPressed)
        k.CurrentState = States.JumpBackward;

    else if (isWPressed && isDPressed)
        k.CurrentState = States.JumpForward;

    else if (isWPressed)
        k.CurrentState = States.Jump;

    else if (isSPressed)
        k.CurrentState = States.CrouchDown;
    
    else if (isDPressed)
        k.CurrentState = States.Forward;
    
    else if (isAPressed)
        k.CurrentState = States.Backward;
    else if (isJPressed)
        k.CurrentState = States.LightPunch;
    else if (isKPressed)
        k.CurrentState = States.MediumPuch;

    if (e.KeyCode == Keys.Right)
        j.CurrentState = States.Forward;
    if (e.KeyCode == Keys.Left)
        j.CurrentState = States.Backward;
    if (e.KeyCode == Keys.Up)
        j.CurrentState = States.Jump;
    if (e.KeyCode == Keys.Down)
        j.CurrentState = States.CrouchDown;

    };

form.KeyUp += (sender, e) => {
    if (e.KeyCode == Keys.A)
        isAPressed = false;
    if (e.KeyCode == Keys.D)
        isDPressed = false;
    if (e.KeyCode == Keys.W)
        isWPressed = false;
    if (e.KeyCode == Keys.S)
    {
        isSPressed = false;
        k.CurrentState = States.CrouchUp;
        return;
    }

    if (e.KeyCode == Keys.Down)
        j.CurrentState = States.CrouchUp;
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

