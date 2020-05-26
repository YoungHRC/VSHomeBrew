using System;
using Tester.Services;

namespace Tester.Models
{
    class AlarmsLinkedList
    {
        private Alarms head;
        public Alarms current;
        private MyContext repoA;
        int size, num;

        public AlarmsLinkedList()
        {
            head = null;
            size = 0;
        }
        public AlarmsLinkedList(Alarms head, int size, MyContext A)
        {
            this.head = head;
            this.size = size;
            this.repoA = A;
            Console.WriteLine("Size " + size);
            PrintAll();
        }

        public void InsertAfter(Alarms prev_node, Alarms new_node)
        {
            if (prev_node == null){
                Console.WriteLine("The given previous node Cannot be null");
                return;
            }
            new_node.setNextAlarmInfo(prev_node.NextAlarm);
            prev_node.setNextAlarmInfo(new_node);
            repoA.SaveAlarmAsync(prev_node);
            repoA.SaveAlarmAsync(new_node);
            repoA.SaveChangesAsync();
            size++;
        }

        internal void RemoveAfter(Alarms prev_node, Alarms toDelete)
        {
            if (prev_node == null)
            {
                Console.WriteLine("The given previous node Cannot be null");
                return;
            }
            prev_node.setNextAlarmInfo(toDelete.NextAlarm);  //or  prev_node.NextAlarm.NextAlarm;
            repoA.DeleteAlarm(toDelete);
            repoA.SaveAlarmAsync(prev_node);
            repoA.SaveChangesAsync();
            size--;
        }

        internal void InsertFront(Alarms new_node)
        {
            new_node.setNextAlarmInfo(head);
            head = new_node;
            head.Head = true;
            head.NextAlarm.Head = false;
            repoA.SaveAlarmAsync(head.NextAlarm);
            repoA.SaveAlarmAsync(head);
            repoA.SaveChangesAsync(); 
            size++;
        }

        internal void RemoveFront()
        {
            Alarms temp = this.head;
            this.head = head.NextAlarm;
            head.Head = true;
            repoA.DeleteAlarm(temp);
            repoA.SaveAlarmAsync(head);
            repoA.SaveChangesAsync();
            size--;
        }

        internal Alarms GetLastNode()
        {
            Alarms temp = head;
            while (temp.NextAlarm != null)
            {
                temp = temp.NextAlarm;
            }
            return temp;
        }

        internal void InsertLast(Alarms new_node)
        {
            if (head == null)
            {
                head = new_node;
                current = head;
                current.Head = true;
            }
            else
            {
                Alarms lastNode = GetLastNode();
                lastNode.setNextAlarmInfo(new_node);
                current = lastNode.NextAlarm;
                repoA.SaveAlarmAsync(lastNode);
            }
            repoA.SaveAlarmAsync(current);
            repoA.SaveChangesAsync();
            size++;
        }

        public int getSize()
        {
            return size;
        }

        public void AddByIndex(Alarms new_node, int index)
        {
            if (index < 0)
                throw new ArgumentOutOfRangeException("Index : " + index);
            if (index > size)
                index = size;

            current = this.head;
            if (size == 0 || index == 0)
                InsertFront(new_node);
            else
            {
                for (int i = 0; i < index - 1; i++)
                    current = current.NextAlarm;
                InsertAfter(current, new_node);
            }
        }

        public void RemoveByIndex(int index)
        {
            if (index < 0 || size == 0)
                throw new ArgumentOutOfRangeException("Index : " + index);
            if (index >= size)
                index = size - 1;

            current = this.head;
            if (index == 0)
                RemoveFront();
            else
            {
                for (int i = 0; i < index - 1; i++)
                    current = current.NextAlarm;
                RemoveAfter(current, current.NextAlarm);
            }
        }

        public Alarms getAlarm(int Index)
        {
            if (Index < 0 || size == 0)
                throw new ArgumentOutOfRangeException("Index : " + Index);
            if (Index >= this.size)
                Index = this.size - 1;

            current = this.head;
            for (int i = 0; i < Index; i++)
            {
                current = current.NextAlarm;
            }
            return current;
        }

        internal void PrintAll()
        {
            num = 0;
            while (head != null)
            {
                Console.WriteLine(num++ + " " + head.stageName + " " + head.Minutes + " " + head.IngredList);
                head = head.NextAlarm;
            }
        }
    }
}
