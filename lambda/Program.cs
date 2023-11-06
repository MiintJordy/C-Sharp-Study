using System.Data;

namespace lambda
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var lambda = new LambdaTest();

            // lambda.SayHello();

            // Console.WriteLine(lambda.Add(1, 2));

            // Console.WriteLine();

            // lambda.LambdaFunc(lambda.SayHello);

            lambda.LambdaFunc(() =>
            {
                Console.WriteLine("Hello");
            });
            Console.Read();
        }

        public class LambdaTest
        {
            // 람다를 사용하는 이유
            // 함수에 함수를 파라미터로 넣고 싶은 생각에 만들어짐
            // C에서는 함수포인터, C#에서는 대리자

            // 람다로 바꾸기 전
            public void SayHello()
            {
                Console.WriteLine("Hello");
            }

            // 1. 접근제어지시자 제거, 반환타입 제거: sayHello()
            // 2. 정의되어있는 곳에서 한 번만 사용하기 때문에 이름이 필요없음 : ()
            // 3. 이것이 람다인 것을 알려주기 위한 방법 " => "
            // () => 
            // {
            //     Console.WriteLine("Hello");
            // }
            
            // 람다로 바꾸기 전
            // public int Add(int a, int b)
            // {
                // return a + b;
            // }

            // 1. 반환값이 필요없음 -> int와 int면 int가 나오겠네
            // (int a, int b) => 
            // {
            //    return a + b;
            // }

            // action 매개변수가 없고 값을 반환하지 않는 메서드를 캡슐화
            public void LambdaFunc(Action action)
            {
                action();
            }
        }
    }
}