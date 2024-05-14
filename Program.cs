using System;

// Стани автомата
interface IState
{
    void SelectItem(VendingMachine vendingMachine);
    void Pay(VendingMachine vendingMachine);
    void DispenseItem(VendingMachine vendingMachine);
}

// Початковий стан автомата
class NoItemState : IState
{
    public void SelectItem(VendingMachine vendingMachine)
    {
        Console.WriteLine("Немає товарів для продажу.");
    }

    public void Pay(VendingMachine vendingMachine)
    {
        Console.WriteLine("Немає товарів для продажу.");
    }

    public void DispenseItem(VendingMachine vendingMachine)
    {
        Console.WriteLine("Немає товарів для продажу.");
    }
}

// Стан автомата після вибору товару
class HasItemState : IState
{
    public void SelectItem(VendingMachine vendingMachine)
    {
        Console.WriteLine("Товар вже обраний.");
    }

    public void Pay(VendingMachine vendingMachine)
    {
        Console.WriteLine("Товар оплачено.");
        vendingMachine.SetState(new SoldState());
    }

    public void DispenseItem(VendingMachine vendingMachine)
    {
        Console.WriteLine("Будь ласка, спочатку оплатіть товар.");
    }
}

// Стан автомата після оплати
class SoldState : IState
{
    public void SelectItem(VendingMachine vendingMachine)
    {
        Console.WriteLine("Товар вже оплачено.");
    }

    public void Pay(VendingMachine vendingMachine)
    {
        Console.WriteLine("Товар вже оплачено.");
    }

    public void DispenseItem(VendingMachine vendingMachine)
    {
        Console.WriteLine("Товар виданий.");
        vendingMachine.SetState(new NoItemState());
    }
}

// Клас автомата
class VendingMachine
{
    private IState currentState;

    public VendingMachine()
    {
        currentState = new NoItemState();
    }

    public void SetState(IState state)
    {
        currentState = state;
    }

    public void SelectItem()
    {
        currentState.SelectItem(this);
    }

    public void Pay()
    {
        currentState.Pay(this);
    }

    public void DispenseItem()
    {
        currentState.DispenseItem(this);
    }
}

class Program
{
    static void Main(string[] args)
    {
        VendingMachine vendingMachine = new VendingMachine();

        // Перехід до стану з товаром
        vendingMachine.SetState(new HasItemState());
        vendingMachine.SelectItem();
        vendingMachine.Pay();
        vendingMachine.DispenseItem();

        // Перехід до стану без товару
        vendingMachine.SetState(new NoItemState());
        vendingMachine.SelectItem();

        // Перехід до стану оплати
        vendingMachine.SetState(new SoldState());
        vendingMachine.SelectItem();
        vendingMachine.Pay();
        vendingMachine.DispenseItem();

        Console.ReadLine();
    }
}