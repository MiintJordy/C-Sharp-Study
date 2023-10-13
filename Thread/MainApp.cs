/*
 궁금증과 해결
 1. async void 형태의 메소드는 메소드 자체가 비동기가 아니고 요소를 비동기로 만들기 위함이다.
  > async void도 비동기다.
 2. async Task 형태의 메소드는 함수 자체를 호출할 때 비동기로 사용하고 싶을 때 하는 것이다.
  > 반환값이 필요할 때하는 것임. 그런 이유에서 await을 사용하지 않고 함수를 호출하면, "너 반환값필요하잖아?"라는 경고가 발생
 3. Task.Run은 비동기를 실행하고자 만든 편의기능 정도로 이해
 */
using System;
using System.Threading.Tasks;

class Program
{
    static void Main()
    {
        Console.WriteLine("메인 메서드 시작");  // 1

        MyAsyncMethod();

        Console.WriteLine("메인 메서드 종료"); // 4
    }

    static async void MyAsyncMethod()  // 함수 자체는 동기지만
    {
        Console.WriteLine("비동기 메서드 시작"); // 2
        await Task.Delay(3000);  // 3 <- await이 비동기로 만든것인가
        Console.WriteLine("비동기 메서드 종료");
    }
}

// 비동기로 작동하겠다는 의미?
using System;
using System.Threading.Tasks;

class Program
{
    static void Main()
    {
        Console.WriteLine("메인 메서드 시작");  // 1

        Task.Run(() => MyAsyncMethod());   // 2

        // 3과 4는 비동기이지만 처리 시간 차이로 정해진건가?

        Console.WriteLine("메인 메서드 종료"); // 3
    }

    static async void MyAsyncMethod()
    {
        Console.WriteLine("비동기 메서드 시작"); // 4
        await Task.Delay(3000);
        Console.WriteLine("비동기 메서드 종료");
    }
}


using System;
using System.Threading.Tasks;

class Program
{
    static async Task Main()
    {
        Console.WriteLine("메인 메서드 시작");   // 1

        // 비동기 메서드 호출 및 기다림
        // Task.Run()을 한 async void와 같은가?
        MyAsyncMethod();   

        Console.WriteLine("메인 메서드 종료"); //4
    }

    static async Task MyAsyncMethod()
    {
        Console.WriteLine("비동기 메서드 시작"); // 2
        
        // 비동기적으로 어떤 작업 수행
        await Task.Delay(2000);   // 3

        Console.WriteLine("비동기 메서드 종료");
    }
}