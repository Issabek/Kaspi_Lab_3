using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Kaspi_Lab_3
{
    public enum Direction
    {
        UP,
        DOWN,
        STOPPED
    }
    class ElevatorQueue //Хотел сделать логику для становления очереди вызова лифта, но понял что это лишнее
    {
        public int floorToGo { set; get; }
        public Direction dir { set; get; }
        public ElevatorQueue(int myFloor, Direction myDir)
        {
            this.floorToGo = myFloor;
            this.dir = myDir;
        }
    }

    public class Elevator
    {
        #region props
        private const int delayMills = 2000;
        public int height { get; set; }
        readonly int carryingCapacity; //Условно скажем там ограничение не по весу а по количеству пассажиров
        public int currentCarry { get; set;}
        public int elevCurrFloor { get;set;}
        private Direction currDir { get; set; } = Direction.STOPPED;
        private List<ElevatorQueue> queue = new List<ElevatorQueue>();
        #endregion

        public void callFromInside()
        {
            Console.Clear();
            Console.WriteLine("На какой этаж вы собираетесь?\n");
            int targetFloor = Int32.Parse(Console.ReadLine());
            char yesNo = 'y';
            if (elevCurrFloor > targetFloor)
            {
                Console.Clear();
                currDir = Direction.DOWN;
                Thread.Sleep(delayMills / 2);
                Console.WriteLine("Лифт спускается");
                for (int i = elevCurrFloor; i > targetFloor; i--)
                {
                    Console.WriteLine("Лифт сейчас на {0} этаже",i);
                    Thread.Sleep(delayMills / 2);
                    Console.WriteLine("...");
                }
                Console.WriteLine("Лифт спустился на {0} этаж", targetFloor);
                currDir = Direction.STOPPED;
                Thread.Sleep(delayMills / 3);
                Console.WriteLine("Двери открываются");
                elevCurrFloor = targetFloor;
                Console.WriteLine("Хотите ли выгрузить пассажиров?\n Type y/n: ");
                yesNo = char.Parse(Console.ReadLine());
                if (yesNo == 'y')
                {
                    Console.WriteLine("Сколько пассажиров Вы хотите выгрузить?");
                    targetFloor = Int32.Parse(Console.ReadLine());
                    passangersUnload(targetFloor);
                }
                Console.ReadKey();
            }
            else
            {
                Console.Clear();
                currDir = Direction.UP;
                Thread.Sleep(delayMills / 2);
                Console.WriteLine("Лифт поднимается");
                for (int i = elevCurrFloor; i < targetFloor; i++)
                {
                    Console.WriteLine("Лифт сейчас на {0} этаже",i);
                    Thread.Sleep(delayMills / 2);
                    Console.WriteLine("...");
                }
                Console.WriteLine("Лифт поднялся на {0} этаж", targetFloor);
                Thread.Sleep(delayMills / 3);
                currDir = Direction.STOPPED;
                
                Console.WriteLine("Двери открываются");
                elevCurrFloor = targetFloor;
                Console.WriteLine("Хотите ли выгрузить пассажиров?\n Type y/n: ");
                yesNo = char.Parse(Console.ReadLine());
                if (yesNo == 'y')
                {
                    Console.WriteLine("Сколько пассажиров Вы хотите выгрузить?");
                    targetFloor = Int32.Parse(Console.ReadLine());
                    passangersUnload(targetFloor);
                }
                Console.ReadKey();
            }

        }
        public void callFromOutside(int yourFloor) //Вызов лифта снаружи
        {
            Console.Clear();
            Console.WriteLine("Идет вызов лифта");
            Thread.Sleep(delayMills);
            char yesNo = 'y';
            if (yourFloor == elevCurrFloor)
            {
                currDir = Direction.STOPPED;
                Console.WriteLine("Двери открываются");
                Thread.Sleep(delayMills );
                Console.WriteLine("Хотите ли загрузить пассажиров\nType y/n: ");
                yesNo = char.Parse(Console.ReadLine());
                if (yesNo == 'y')
                {
                    Console.WriteLine("Сколько пассажиров собирается в лифт?");
                    int tempVal = Int32.Parse(Console.ReadLine());
                    passangersLoad(tempVal);
                    callFromInside();
                }
                else
                {
                    Console.WriteLine("Всего доброго!");
                    Console.ReadKey();
                }
            }
            else
            {
                if (elevCurrFloor > yourFloor)
                {
                    currDir = Direction.DOWN;
                    Thread.Sleep(delayMills / 2);
                    Console.WriteLine("Лифт спускается");
                    for (int i = elevCurrFloor; i > yourFloor; i--)
                    {
                        Console.WriteLine("Лифт сейчас на {0} этаже",i);
                        Thread.Sleep(delayMills / 2);
                        Console.WriteLine("...");
                    }
                    Console.WriteLine("Лифт спустился на {0} этаж", yourFloor);
                    currDir = Direction.STOPPED;
                    Thread.Sleep(delayMills / 2);
                    elevCurrFloor = yourFloor;
                    callFromOutside(yourFloor);
                }
                else
                {
                    currDir = Direction.UP;
                    Thread.Sleep(delayMills / 2);
                    Console.WriteLine("Лифт поднимается");
                    for (int i = elevCurrFloor; i < yourFloor; i++)
                    {
                        Console.WriteLine("Лифт сейчас на {0} этаже",i);
                        Thread.Sleep(delayMills / 2);
                        Console.WriteLine("...");
                    }
                    Console.WriteLine("Лифт поднялся на {0} этаж", yourFloor);
                    currDir = Direction.STOPPED;
                    Thread.Sleep(delayMills / 2);
                    elevCurrFloor = yourFloor;
                    callFromOutside(yourFloor);
                }

            }
        }
        private void passangersLoad(int passangersCount)
        {
            Console.Clear();
            if (currentCarry + passangersCount <= carryingCapacity && currDir == Direction.STOPPED)
            {

                Console.WriteLine("Идет загрузка пассажиров");
                currentCarry = passangersCount;
                Thread.Sleep(delayMills);
                Console.WriteLine("Загрузка завершена");
                Thread.Sleep(delayMills);
                Console.WriteLine("Двери закрываются");

            }
            else if (currentCarry + passangersCount > carryingCapacity && currDir == Direction.STOPPED)
            {
                Console.WriteLine("Лифт перегружен");
                Console.ReadKey() ;
            }
            else
            {
                Console.WriteLine("Лифт находится в движении");
                Console.ReadKey();
            }
        }
        private void passangersUnload(int passangersCount)
        {
            Console.Clear();
            if (currentCarry - passangersCount >=0 && currDir == Direction.STOPPED)
            {
                Console.WriteLine("Идет выгрузка пассажиров");
                Thread.Sleep(delayMills);
                Console.WriteLine("Выгрузка завершена");
                Thread.Sleep(delayMills);
                Console.WriteLine("Двери закрываются");
            }
            else if (currentCarry - passangersCount < 0 && currDir == Direction.STOPPED)
            {
                Console.WriteLine("Неверное число выходящих пассажиров");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("Лифт находится в движении, выгрузка невозможна!");
                Console.ReadKey();
            }
        }


        public Elevator()
        {
        }
        public Elevator(int height, int capacity, int currentFlo=1)
        {
            this.carryingCapacity = capacity;
            this.height = height;
            if (currentFlo < 1)
                Console.WriteLine("Минусовые этажи еще на стройке");
            else if (currentFlo > height)
                Console.WriteLine("Щас потолок пробьешь");
            else
                this.elevCurrFloor = currentFlo;
        }

    }
}
