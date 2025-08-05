using Simple_Contact_Management_System___Index___Overload;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

class Program
{
    static ContactList contactList = new ContactList();

    static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("Contact Manager");
            Console.WriteLine("1. Add Contact");
            Console.WriteLine("2. Remove Contact");
            Console.WriteLine("3. Display Contacts");
            Console.WriteLine("4. Exit");
            Console.Write("Select an option: ");
            var option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    AddContact();
                    break;
                case "2":
                    RemoveContact();
                    break;
                case "3":
                    DisplayContacts();
                    break;
                case "4":
                    return;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }
    }

    static void AddContact()
    {
        Console.WriteLine("Adding a new contact...");
        string name = PromptForInput("Enter name:", IsNotEmpty);
        string phoneNumber = PromptForInput("Enter phone number (e.g., 123-456-7890):", IsValidPhoneNumber);
        string email = PromptForInput("Enter email:", IsNotEmpty);

        contactList.Add(new Contact(name, phoneNumber, email));
        Console.WriteLine("Contact added successfully.");
    }

    static void RemoveContact()
    {
        string name = PromptForInput("Enter the name of the contact to remove:", IsNotEmpty);
        foreach (var contact in contactList)
        {
            if (contact.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
            {
                if (contactList.Remove(contact))
                {
                    Console.WriteLine("Contact removed successfully.");
                    return;
                }
            }
        }
        Console.WriteLine("Contact not found.");
    }

    static void DisplayContacts()
    {
        Console.WriteLine("Contacts:");
        foreach (var contact in contactList)
        {
            Console.WriteLine(contact);
        }
    }

    static string PromptForInput(string message, Func<string, bool> validator)
    {
        string input;
        do
        {
            Console.Write(message);
            input = Console.ReadLine();
            if (!validator(input))
            {
                Console.WriteLine("Invalid input. Please try again.");
            }
        } while (!validator(input));

        return input;
    }

    static bool IsNotEmpty(string input)
    {
        return !string.IsNullOrWhiteSpace(input);
    }

    static bool IsValidPhoneNumber(string input)
    {
        return Regex.IsMatch(input, @"^\d{3}-\d{3}-\d{4}$");
    }
}
