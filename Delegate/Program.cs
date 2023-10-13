using System.Reflection.Metadata.Ecma335;

namespace Delegate
{
    internal partial class Program
    {

        // 함수의 매개 변수 사용법 1
        // 매개변수를 통해 값 전달
        int num1 = 1;
        int num2 = 2;
        int Add_var(int a, int b)
        {
            int result = a + b;

            return result;
        }

        // 함수의 매개 변수 사용법 2
        // 매개변수 자리에 객체 변수로 값 전달
        class object_params
        {
            public int a;
            public int b;
        }

        int Add_obj(object_params obj)
        {
            int result = 0;

            result = (int)obj.a + (int)obj.b;

            return result;
        }

        // 함수의 매개 변수 사용법 3
        // 매개변수에 대리자 변수를 사용해 로직 전달
        delegate int A(int a, int b);

        class MainClass
        {
            static int Add_var(int a, int b)
            {
                int result = 0;

                result = a + b;

                return result;
            }

            static void Main(string[] args)
            {
                A del = new A(Add_var);

                int res = Add_dele(del);
                Console.WriteLine($"매개변수 사용법:{res}");
            }

            static int Add_dele(A func)
            {
                int result = 0;

                result = func(1, 2);

                return result;
            }
        }
    }

    internal partial class Program
    {
        // 1. 선언 방법
        // delegate + 함수반환형 + 델리게이트 이름 + 시그니처
        delegate return_type delegate_name(int num1, int num2);

        // .NET Frame 1.0
        delegate return_type delegate_name = new delegate (function);

        // .Net Frame 2.0
        delegate return_type delegate_name = function;


        // 예제
        delegate void A(int i);

        class MainClass
        {
            static void Run1(int val)
            {
                Console.WriteLine("{0}", val);
            }

            static void Run2(int value)
            {
                Console.WriteLine("0x{0:X}", value);
            }
            static void Main(string[] args)
            {
                A run = new A(Run1);
                run(1024);

                run = Run2;
                run(2048);
            }
        }
    }

    internal partial class Program
    {
        delegate void Del(string sender);
        class Program
        {
            static void Main(string[] args)
            {
                Program program = new Program();

                Del Multi_delegate = new Del(program.run);
                Multi_delegate += somethinghappend;
                Multi_delegate += myareaclick;
                Multi_delegate += afterclick;
            }

            void run(string sender)
            {
                Console.WriteLine(sender + "run 클릭!");
            }

            void somethinghappend(string sender)
            {
                Console.WriteLine(sender + "somethinghappend 클릭!");
            }
            void myareaclick(string sender)
            {
                Console.WriteLine(sender + "myareaclick 클릭 !");
            }
            void afterclick(string sender)
            {
                Console.WriteLine(sender + "afterclick 클릭!");
            }
        }
    }

    internal partial class Program 
    {
        delegate int A(String str);

        class Temp
        {
            public event A EventHandler;

            public void Func(string msg)
            {
                EventHandler(msg);
            }
        }
    }

    internal partial class Program
    {
        delegate void A(string sender);

        class first
        {
            private string title;
            public Label1(string name)
            {
                this.title = name;
            }

            // label의 텍스트가 변한다고 가정
            public void run(string run_msg)
            {
                Console.WriteLine(run_msg + title + "에서 출력!");
            }
        }

        class second
        {
            static void Main(string[] args)
            {
                Label lbl1 = new Label1("레이블1");
                Label lbl2 = new Label1("레이블2");
                Label lbl3 = new Label1("레이블3");
                Label lbl4 = new Label1("레이블1");

                button btn = new button();

                btn.btn_EventHandler += lbl1.run;
                btn.btn_EventHandler += lbl2.run;
                btn.btn_EventHandler += lbl3.run;
                btn.btn_EventHandler += lbl4.run;

                btn.RunEvent();
            }
        }

        class button
        {
            public event A btn_EventHandler;

            public void RunEvent()
            {
                btn_EventHandler("이벤트 발생");
            }
        }
    }
}

