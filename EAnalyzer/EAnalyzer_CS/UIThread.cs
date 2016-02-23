using System.Threading;
using EAnalyzer_CS;

public class UIThread
{
    private EAForm form;
    private Thread thread;

    public UIThread(EAForm form, ThreadStart startImpl)
    {
        this.form = form;
        this.thread = new Thread(startImpl);
    }

    public EAForm Form
    {
        get
        {
            return form;
        }
    }

    public Thread Thread
    {
        get
        {
            return thread;
        }
    }

    public void Start()
    {
        thread.Start();
    }
}