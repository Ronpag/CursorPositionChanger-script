using System.Runtime.InteropServices;

namespace CursorPositionChanger;

class Program
{ 
    [DllImport("user32.dll")]
    static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);

    [DllImport("user32.dll")]
    static extern bool GetMessage(out MSG lpMsg, IntPtr hWnd, uint wMsgFilterMin, uint wMsgFilterMax);
    
    [DllImport("user32.dll")]
    static extern bool SetCursorPos(int x, int y);
    
    [DllImport("user32.dll")]
    static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint dwData, UIntPtr dwExtraInfo);
    [DllImport("user32.dll")]
    static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, UIntPtr dwExtraInfo);
    
    const int WM_HOTKEY = 0x0312;
    
    const uint MOUSEEVENTF_LEFTDOWN = 0x0002;
    const uint MOUSEEVENTF_LEFTUP = 0x0004;
    
    const int HK1 = 1, HK2 = 2, HK3 = 3, HK4 = 4, HK5 = 5, 
        HKD1 = 6, HKD2 = 7, HKD3 = 8, HKD4 = 9,  HKD5 = 10;

    const byte KBD1 = 0x35, KBD2 = 0x37, KBD3 = 0x33;

    static void Main()
    {
        Console.WriteLine("Starting cursor position changer...");
        RegisterHotKey(IntPtr.Zero, HK1, 0,(uint)ConsoleKey.Tab); 
        RegisterHotKey(IntPtr.Zero, HK2, 0,(uint)ConsoleKey.Q);
        RegisterHotKey(IntPtr.Zero, HK3, 0,(uint)ConsoleKey.W);
        RegisterHotKey(IntPtr.Zero, HK4, 0,(uint)ConsoleKey.E);
        RegisterHotKey(IntPtr.Zero, HK5, 0,(uint)ConsoleKey.D);
        RegisterHotKey(IntPtr.Zero, HKD1, 0, (uint)ConsoleKey.Spacebar); //5
        RegisterHotKey(IntPtr.Zero, HKD2, 0, (uint)ConsoleKey.F); //7
        RegisterHotKey(IntPtr.Zero, HKD3, 0, (uint)ConsoleKey.R); //3

        
        MSG msg;

        while (GetMessage(out msg, IntPtr.Zero, 0, 0))
        {
            if (msg.message == WM_HOTKEY)
            {
                int hotkeyId = (int)msg.wParam;

                switch (hotkeyId)
                {
                    case HK1:
                        click(750, 243);
                        break;
                    case HK2:
                        click(450, 427);
                        break;
                    case HK3:
                        click(690, 505);
                        break;
                    case HK4:
                        click(690, 585);
                        break;
                    case HK5:
                        click(690, 440);
                        break;
                    case HKD1:
                        autoClick(KBD1);
                        break;
                    case HKD2:
                        autoClick(KBD2);
                        break;
                    case HKD3:
                        autoClick(KBD3);
                        break;
                }
            }
        }
    }

    static void click(int x, int y)
    {
        SetCursorPos(x, y);
        
        Thread.Sleep(50);
        mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, UIntPtr.Zero);
        mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, UIntPtr.Zero);
    }

    static void autoClick(byte KB)
    {
        SetCursorPos(460, 455);
        Thread.Sleep(50);
        mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, UIntPtr.Zero);
        
        Thread.Sleep(20);
        keybd_event(0x30, 0, 0x0000, UIntPtr.Zero);
        keybd_event(0x30, 0, 0x0002, UIntPtr.Zero);
        
        Thread.Sleep(50);
        keybd_event(KB, 0, 0x0000, UIntPtr.Zero);
        keybd_event(KB, 0, 0x0002, UIntPtr.Zero);
        
        Thread.Sleep(20);
        keybd_event(0x0D, 0, 0x0000, UIntPtr.Zero);
        keybd_event(0x0D, 0, 0x0002, UIntPtr.Zero);
        Thread.Sleep(20);
        
        mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, UIntPtr.Zero);
        click(690, 585);
        SetCursorPos(1000, 330);
    }
}

public struct MSG
{
    public IntPtr hwnd;
    public uint message;
    public UIntPtr wParam;
    public IntPtr lParam;
    public uint time;
}