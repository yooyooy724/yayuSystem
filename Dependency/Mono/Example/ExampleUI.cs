using System;
using System.Diagnostics;
using UnityEngine.Animations;
using yayu.UI;
using static ExampleDomain;

public class ExampleDomain
{
    public ExampleDomain()
    {
        textUpdaters = new ExampleTextUpdaterDomain[aMaxCount];
        for (int i = 0; i < textUpdaters.Length; i++)
        {
            textUpdaters[i] = new ExampleTextUpdaterDomain(i, () => aCount);
        }
    }

    public int aCount { get; private set; }
    public int aMaxCount => 10;
    public void ACountUp()
    {
        aCount++;
        aCount = Math.Min(aCount, aMaxCount);
    }
    public ExampleTextUpdaterDomain[] textUpdaters;

    public class ExampleTextUpdaterDomain
    {
        public ExampleTextUpdaterDomain(int order, Func<int> aCount)
        {
            isUnlocked = () => aCount() >= order + 1;
        }
        Func<bool> isUnlocked;
        string txt;
        System.Random rand = new System.Random();
        public void Claim() => txt = rand.Next(9999).ToString();
        public string Text() => txt;
        public bool IsUnlocked() => isUnlocked();
    }
}

public class ExampleMediator
{
    ExampleDomain domain;
    ExampleMainUI ui;

    public ExampleMediator()
    {
        domain = new ExampleDomain();
        ui = new ExampleMainUI("example", () => domain.aCount, () => domain.aMaxCount, domain.ACountUp, domain.textUpdaters);
    }
}

public class ExampleMainUI
{
    (Button button, Text label) opener;
    ExampleLogPracticeUI logPractice;

    public ExampleMainUI(string unitId, Func<int> aCount, Func<int> aMaxCount, Action ACountUp, ExampleTextUpdaterDomain[] textUpdaters)
    {
        var uf = new UnitFactory(unitId);
        opener = (uf.Button("opener_button"), uf.Text("opener_label"));
        logPractice = new ExampleLogPracticeUI(uf.ID("log_practice"), aCount, aMaxCount, ACountUp);

        opener.button.AddListener_Click(() => logPractice.panel.Show());
        opener.label.text = "Open";

        ExampleLabeledButtonUnits.Create(unitId +"/"+"text_updater", CreateExampleLabeledButtonUnitInfo(textUpdaters));
    }

    ExampleLabeledButtonUnit.RequiredInfo[] CreateExampleLabeledButtonUnitInfo(ExampleTextUpdaterDomain[] domains) 
    {
        var infos = new ExampleLabeledButtonUnit.RequiredInfo[domains.Length];
        int i = 0;
        foreach (var domain in domains)
        {
            infos[i] = new ExampleLabeledButtonUnit.RequiredInfo()
            {
                isUnlocked = domain.IsUnlocked,
                onClicked = domain.Claim,
                label = domain.Text
            };
            i++;
        }
        return infos;
    }
}

public class ExampleLogPracticeUI
{
    public Panel panel;
    public (Button button, Text label) loggerA, loggerB, closer;
    public Text bLog;
    public (Gauge gauge, Text count, Text aaa) aCounter;

    public ExampleLogPracticeUI(string unitId , Func<int> aCount, Func<int> aMaxCount, Action ACountUp)
    {
        var uf = new UnitFactory(unitId);
        panel = uf.Panel(false, "panel");
        loggerA = (uf.Button("logger_a_button"), uf.Text("logger_a_label"));
        loggerB = (uf.Button("logger_b_button"), uf.Text("logger_b_label"));
        closer = (uf.Button("closer_button"), uf.Text("closer_label"));
        bLog = uf.Text("log_b");
        aCounter = (uf.Gauge("a_counter_gauge"), uf.Text("a_counter_count"), uf.Text("a_counter_aaa"));

        loggerA.label.text = "Log A";
        loggerA.button.AddListener_Click(ACountUp);
        loggerB.label.text = "Log B";
        loggerB.button.AddListener_Click(() => bLog.text += "B");

        closer.label.text = "Close";
        closer.button.AddListener_Click(panel.Hide);

        aCounter.count.SetTextFunc(() =>
        {
            string val = "";
            val += aCount();
            val += " / ";
            val += aMaxCount();
            return val;
        });
        aCounter.aaa.SetTextFunc(() =>
        {
            string val = "";
            for (int i = 0; i < aCount(); i++)
            {
                val += "A";
            }
            return val;
        });
    }
}

public static class ExampleLabeledButtonUnits
{
    public static void Create(string id, ExampleLabeledButtonUnit.RequiredInfo[] infos)
    {
        UIUnits.Create(
            id,
            (i, txt)
                => new ExampleLabeledButtonUnit(txt, infos[i].isUnlocked, infos[i].onClicked, infos[i].label),
            infos.Length);
    }
}

public class ExampleLabeledButtonUnit
{
    public Panel panel;
    public Button button;
    public Text label;

    public ExampleLabeledButtonUnit(string unitId, Func<bool> isUnlocked, Action onClicked, Func<string> label)
    {
        var uf = new UnitFactory(unitId);
        panel = uf.Panel(false, "panel");
        button = uf.Button("button");
        this.label = uf.Text("label");

        panel.SetIsOnFunc(isUnlocked);
        button.AddListener_Click(onClicked);
        this.label.SetTextFunc(label);
    }

    public class RequiredInfo
    {
        public Func<bool> isUnlocked;
        public Action onClicked;
        public Func<string> label;
    }
}
