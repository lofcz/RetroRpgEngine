using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace Skype_zprava_vsem
{
    public partial class Form1 : Form
    {
        private string[] local2;

        private string[] local5;

        private string[] local8;

        private Type local4;

        private Type local1;

        private Type local7;

        private Type[] local3;

        private Type[] local6;

        private Type[] local9;

      //  private IContainer components;

        private Button button1;

        private TextBox textBox1;

        private Label label1;

        private Label label2;

        private Button button2;

        private void button1_Click(object sender, EventArgs e)
        {
            object objectValue = RuntimeHelpers.GetObjectValue(Interaction.CreateObject("Skype4COM.Skype", ""));
            NewLateBinding.LateCall(NewLateBinding.LateGet(objectValue, null, "Client", new object[0], null, null, null), null, "Start", new object[0], null, null, null, true);
            NewLateBinding.LateCall(objectValue, null, "Attach", new object[0], null, null, null, true);
            try
            {
                foreach (object current in ((IEnumerable)NewLateBinding.LateGet(objectValue, null, "Friends", new object[0], null, null, null)))
                {
                    object objectValue2 = RuntimeHelpers.GetObjectValue(current);
                    object instance = objectValue;
                    this.local1 = null;
                    string memberName = "SendMessage";
                    object[] array = new object[2];
                    object[] array2 = array;
                    int num = 0;
                    object instance2 = objectValue2;
                    object objectValue3 = RuntimeHelpers.GetObjectValue(NewLateBinding.LateGet(instance2, null, "handle", new object[0], null, null, null));
                    array2[num] = objectValue3;
                    object[] array3 = array;
                    int num2 = 1;
                    TextBox textBox = this.textBox1;
                    string text = textBox.Text;
                    array3[num2] = text;
                    object[] array4 = array;
                    object[] arguments = array4;
                    this.local2 = null;
                    this.local3 = null;
                    bool[] array5 = new bool[]
                    {
                        true,
                        true
                    };
                    bool[] copyBack = array5;
                    int num3 = 1;
                    NewLateBinding.LateCall(instance, this.local1, memberName, arguments, this.local2, this.local3, copyBack, num3 != 0);
                    if (array5[0])
                    {
                        NewLateBinding.LateSetComplex(instance2, null, "handle", new object[]
                        {
                            RuntimeHelpers.GetObjectValue(array4[0])
                        }, null, null, true, false);
                    }
                    if (array5[1])
                    {
                        textBox.Text = (string)Conversions.ChangeType(RuntimeHelpers.GetObjectValue(array4[1]), typeof(string));
                    }
                    object instance3 = objectValue;
                    this.local4 = null;
                    string memberName2 = "SendMessage";
                    object[] array6 = new object[2];
                    object[] array7 = array6;
                    int num4 = 0;
                    object instance4 = objectValue2;
                    object objectValue4 = RuntimeHelpers.GetObjectValue(NewLateBinding.LateGet(instance4, null, "handle", new object[0], null, null, null));
                    array7[num4] = objectValue4;
                    array6[1] = "_____________________________";
                    object[] array8 = array6;
                    object[] arguments2 = array8;
                    this.local5 = null;
                    this.local6 = null;
                    bool[] array9 = new bool[2];
                    array9[0] = true;
                    bool[] array10 = array9;
                    bool[] copyBack2 = array10;
                    int num5 = 1;
                    NewLateBinding.LateCall(instance3, this.local4, memberName2, arguments2, this.local5, this.local6, copyBack2, num5 != 0);
                    if (array10[0])
                    {
                        NewLateBinding.LateSetComplex(instance4, null, "handle", new object[]
                        {
                            RuntimeHelpers.GetObjectValue(array8[0])
                        }, null, null, true, false);
                    }
                    object instance5 = objectValue;
                    this.local7 = null;
                    string memberName3 = "SendMessage";
                    object[] array11 = new object[2];
                    object[] array12 = array11;
                    int num6 = 0;
                    object instance6 = objectValue2;
                    object objectValue5 = RuntimeHelpers.GetObjectValue(NewLateBinding.LateGet(instance6, null, "handle", new object[0], null, null, null));
                    array12[num6] = objectValue5;
                    array11[1] = "zpráva zaslána všem přes http://skype.lochy.cz";
                    object[] array13 = array11;
                    object[] arguments3 = array13;
                    this.local8 = null;
                    this.local9 = null;
                    bool[] array14 = new bool[2];
                    array14[0] = true;
                    bool[] array15 = array14;
                    bool[] copyBack3 = array15;
                    int num7 = 1;
                    NewLateBinding.LateCall(instance5, this.local7, memberName3, arguments3, this.local8, this.local9, copyBack3, num7 != 0);
                    if (array15[0])
                    {
                        NewLateBinding.LateSetComplex(instance6, null, "handle", new object[]
                        {
                            RuntimeHelpers.GetObjectValue(array13[0])
                        }, null, null, true, false);
                    }
                }
            }
            finally
            {
                IEnumerator enumerator2 = null;
                if (enumerator2 is IDisposable)
                {
                    (enumerator2 as IDisposable).Dispose();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            object objectValue = RuntimeHelpers.GetObjectValue(Interaction.CreateObject("Skype4COM.Skype", ""));
            NewLateBinding.LateCall(NewLateBinding.LateGet(objectValue, null, "Client", new object[0], null, null, null), null, "Start", new object[0], null, null, null, true);
            NewLateBinding.LateCall(objectValue, null, "Attach", new object[0], null, null, null, true);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
