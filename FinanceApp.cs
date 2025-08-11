using System.Linq;

namespace FinanceSystem
{
    /// <summary>
    /// Main application class responsible for coordinating the finance management system
    /// </summary>
    public class FinanceApp
    {
        private List<Transaction> _transactions;

        /// <summary>
        /// Initializes the FinanceApp with an empty transaction list
        /// </summary>
        public FinanceApp()
        {
            _transactions = new List<Transaction>();
        }

        /// <summary>
        /// Main execution method that runs the interactive finance management system
        /// </summary>
        public void Run()
        {
            Console.WriteLine("=== Finance Management System ===\n");

            // 1. Create a SavingsAccount with initial balance
            var savingsAccount = new SavingsAccount("SAV-001", 1000);
            Console.WriteLine($"Created savings account: {savingsAccount.AccountNumber} with initial balance: ${savingsAccount.Balance:F2}\n");

            // 2. Create transaction processors
            var mobileMoneyProcessor = new MobileMoneyProcessor();
            var bankTransferProcessor = new BankTransferProcessor();
            var cryptoWalletProcessor = new CryptoWalletProcessor();

            bool continueProcessing = true;
            int transactionId = 1;

            while (continueProcessing)
            {
                try
                {
                    Console.WriteLine("=== Main Menu ===");

                    // Get operation type from user
                    var operationType = GetOperationType();

                    switch (operationType)
                    {
                        case 0: // Exit
                            continueProcessing = false;
                            continue;

                        case 1: // Make Transaction (Withdrawal)
                            ProcessWithdrawalTransaction(savingsAccount, mobileMoneyProcessor, bankTransferProcessor, cryptoWalletProcessor, ref transactionId);
                            break;

                        case 2: // Deposit Money
                            ProcessDeposit(savingsAccount);
                            break;

                        case 3: // Check Balance
                            savingsAccount.CheckBalance();
                            break;

                        case 4: // View Transaction History
                            ViewTransactionHistory();
                            break;

                        default:
                            Console.WriteLine("Invalid operation selected.");
                            break;
                    }

                    if (continueProcessing && operationType != 0)
                    {
                        // Ask if user wants to continue
                        continueProcessing = AskToContinue();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}\n");
                    continueProcessing = AskToContinue();
                }
            }

            // Display final summary
            DisplayTransactionSummary(savingsAccount);
        }

        /// <summary>
        /// Processes a withdrawal transaction
        /// </summary>
        /// <param name="savingsAccount">The account to process the transaction on</param>
        /// <param name="mobileMoneyProcessor">Mobile money processor</param>
        /// <param name="bankTransferProcessor">Bank transfer processor</param>
        /// <param name="cryptoWalletProcessor">Crypto wallet processor</param>
        /// <param name="transactionId">Reference to the current transaction ID</param>
        private void ProcessWithdrawalTransaction(SavingsAccount savingsAccount,
            MobileMoneyProcessor mobileMoneyProcessor,
            BankTransferProcessor bankTransferProcessor,
            CryptoWalletProcessor cryptoWalletProcessor,
            ref int transactionId)
        {
            Console.WriteLine("=== Withdrawal Transaction ===");

            // Get withdrawal account type
            var accountType = GetWithdrawalAccountType();
            if (accountType == 0) // Cancel option
            {
                Console.WriteLine("Transaction cancelled.\n");
                return;
            }

            // Get transaction type from user
            var processorType = GetTransactionType();
            if (processorType == 0) // Cancel option
            {
                Console.WriteLine("Transaction cancelled.\n");
                return;
            }

            // Process specific transaction type with enhanced options
            bool transactionSuccess = false;
            decimal amount = 0;
            string category = "";

            switch (processorType)
            {
                case 1: // Mobile Money Transfer
                    (transactionSuccess, amount, category) = ProcessMobileMoneyTransfer();
                    break;
                case 2: // Bank Transfer
                    (transactionSuccess, amount, category) = ProcessBankTransfer();
                    break;
                case 3: // Crypto Wallet Transfer
                    (transactionSuccess, amount, category) = ProcessCryptoTransfer();
                    break;
            }

            if (!transactionSuccess)
            {
                Console.WriteLine("Transaction cancelled or failed.\n");
                return;
            }

            // Create transaction
            var transaction = new Transaction(transactionId++, DateTime.Now, amount, category);

            // Select and use the appropriate processor
            ITransactionProcessor processor = processorType switch
            {
                1 => mobileMoneyProcessor,
                2 => bankTransferProcessor,
                3 => cryptoWalletProcessor,
                _ => throw new InvalidOperationException("Invalid processor type")
            };

            Console.WriteLine($"\n--- Processing Transaction {transaction.Id} ---");
            processor.Process(transaction);
            savingsAccount.ApplyTransaction(transaction);
            _transactions.Add(transaction);

            Console.WriteLine("Transaction completed successfully!\n");
        }

        /// <summary>
        /// Processes a deposit operation with detailed options
        /// </summary>
        /// <param name="account">The account to deposit money into</param>
        private void ProcessDeposit(SavingsAccount account)
        {
            Console.WriteLine("=== Deposit Money ===");

            // Get deposit method
            int depositMethod = GetDepositMethod();
            if (depositMethod == 0)
            {
                Console.WriteLine("Deposit cancelled.\n");
                return;
            }

            decimal amount = GetDepositAmount();
            if (amount <= 0)
            {
                Console.WriteLine("Deposit cancelled.\n");
                return;
            }

            bool success = false;

            switch (depositMethod)
            {
                case 1: // Mobile Money
                    success = ProcessMobileMoneyDeposit(amount);
                    break;
                case 2: // Bank Transfer
                    success = ProcessBankDeposit(amount);
                    break;
                case 3: // Crypto Wallet
                    success = ProcessCryptoDeposit(amount);
                    break;
            }

            if (success)
            {
                account.Deposit(amount);
                Console.WriteLine("Deposit completed successfully!\n");
            }
            else
            {
                Console.WriteLine("Deposit cancelled or failed.\n");
            }
        }

        /// <summary>
        /// Displays the complete transaction history
        /// </summary>
        private void ViewTransactionHistory()
        {
            Console.WriteLine("=== Transaction History ===");

            if (_transactions.Count == 0)
            {
                Console.WriteLine("No transactions found. Your transaction history is empty.\n");
                return;
            }

            Console.WriteLine($"Total Transactions: {_transactions.Count}");
            Console.WriteLine($"Total Amount Spent: ${_transactions.Sum(t => t.Amount):F2}\n");

            Console.WriteLine("Transaction Details:");
            Console.WriteLine("─".PadRight(80, '─'));
            Console.WriteLine($"{"ID",-5} {"Date",-20} {"Amount",-12} {"Category",-20}");
            Console.WriteLine("─".PadRight(80, '─'));

            foreach (var transaction in _transactions.OrderByDescending(t => t.Date))
            {
                Console.WriteLine($"{transaction.Id,-5} {transaction.Date:yyyy-MM-dd HH:mm:ss,-20} " +
                                $"${transaction.Amount,-11:F2} {transaction.Category,-20}");
            }

            Console.WriteLine("─".PadRight(80, '─'));

            // Group transactions by category for summary
            var categoryTotals = _transactions
                .GroupBy(t => t.Category)
                .Select(g => new { Category = g.Key, Total = g.Sum(t => t.Amount), Count = g.Count() })
                .OrderByDescending(x => x.Total);

            Console.WriteLine("\nSpending by Category:");
            Console.WriteLine("─".PadRight(50, '─'));
            Console.WriteLine($"{"Category",-20} {"Count",-8} {"Total Amount",-15}");
            Console.WriteLine("─".PadRight(50, '─'));

            foreach (var categoryTotal in categoryTotals)
            {
                Console.WriteLine($"{categoryTotal.Category,-20} {categoryTotal.Count,-8} ${categoryTotal.Total,-14:F2}");
            }

            Console.WriteLine("─".PadRight(50, '─'));
            Console.WriteLine();
        }

        /// <summary>
        /// Gets a valid deposit amount from user input
        /// </summary>
        /// <returns>The deposit amount (0 if cancelled)</returns>
        private decimal GetDepositAmount()
        {
            while (true)
            {
                Console.Write("Enter deposit amount ($) or 0 to cancel: ");
                var input = Console.ReadLine();

                if (decimal.TryParse(input, out decimal amount))
                {
                    if (amount == 0)
                    {
                        return 0; // User cancelled
                    }
                    if (amount > 0)
                    {
                        return amount;
                    }
                }

                Console.WriteLine("Invalid amount. Please enter a positive number or 0 to cancel.");
            }
        }

        /// <summary>
        /// Gets the operation type selection from the user
        /// </summary>
        /// <returns>Integer representing the selected operation type (0 to exit)</returns>
        private int GetOperationType()
        {
            while (true)
            {
                Console.WriteLine("Select operation:");
                Console.WriteLine("1. Make a Transaction (Withdrawal)");
                Console.WriteLine("2. Deposit Money");
                Console.WriteLine("3. Check Balance");
                Console.WriteLine("4. View Transaction History");
                Console.WriteLine("0. Exit");
                Console.Write("Enter your choice (0-4): ");

                var input = Console.ReadLine();
                if (int.TryParse(input, out int choice) && choice >= 0 && choice <= 4)
                {
                    return choice;
                }

                Console.WriteLine("Invalid choice. Please enter a number between 0 and 4.\n");
            }
        }

        /// <summary>
        /// Gets the transaction type selection from the user for withdrawals
        /// </summary>
        /// <returns>Integer representing the selected transaction type (0 to cancel)</returns>
        private int GetTransactionType()
        {
            while (true)
            {
                Console.WriteLine("Select transaction type:");
                Console.WriteLine("1. Mobile Money Transfer");
                Console.WriteLine("2. Bank Transfer");
                Console.WriteLine("3. Crypto Wallet Transfer");
                Console.WriteLine("0. Cancel");
                Console.Write("Enter your choice (0-3): ");

                var input = Console.ReadLine();
                if (int.TryParse(input, out int choice) && choice >= 0 && choice <= 3)
                {
                    return choice;
                }

                Console.WriteLine("Invalid choice. Please enter a number between 0 and 3.\n");
            }
        }

        /// <summary>
        /// Gets transaction details from user input
        /// </summary>
        /// <param name="transactionId">The ID for the new transaction</param>
        /// <returns>A new Transaction object or null if input was invalid</returns>
        private Transaction GetTransactionDetails(int transactionId)
        {
            try
            {
                // Get transaction amount
                decimal amount = GetTransactionAmount();
                if (amount <= 0)
                {
                    Console.WriteLine("Transaction cancelled - invalid amount.\n");
                    return null;
                }

                // Get transaction category
                string category = GetTransactionCategory();
                if (string.IsNullOrWhiteSpace(category))
                {
                    Console.WriteLine("Transaction cancelled - invalid category.\n");
                    return null;
                }

                // Create transaction with current date
                return new Transaction(transactionId, DateTime.Now, amount, category);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating transaction: {ex.Message}\n");
                return null;
            }
        }

        /// <summary>
        /// Gets a valid transaction amount from user input
        /// </summary>
        /// <returns>The transaction amount</returns>
        private decimal GetTransactionAmount()
        {
            while (true)
            {
                Console.Write("Enter transaction amount ($): ");
                var input = Console.ReadLine();

                if (decimal.TryParse(input, out decimal amount) && amount > 0)
                {
                    return amount;
                }

                Console.WriteLine("Invalid amount. Please enter a positive number.");
            }
        }

        /// <summary>
        /// Gets a valid transaction category from user input
        /// </summary>
        /// <returns>The transaction category</returns>
        private string GetTransactionCategory()
        {
            while (true)
            {
                Console.WriteLine("\nSelect transaction category:");
                Console.WriteLine("1. Groceries");
                Console.WriteLine("2. Utilities");
                Console.WriteLine("3. Entertainment");
                Console.WriteLine("4. Transportation");
                Console.WriteLine("5. Healthcare");
                Console.WriteLine("6. Shopping");
                Console.WriteLine("7. Other (custom)");
                Console.Write("Enter your choice (1-7): ");

                var input = Console.ReadLine();
                if (int.TryParse(input, out int choice))
                {
                    return choice switch
                    {
                        1 => "Groceries",
                        2 => "Utilities",
                        3 => "Entertainment",
                        4 => "Transportation",
                        5 => "Healthcare",
                        6 => "Shopping",
                        7 => GetCustomCategory(),
                        _ => null
                    };
                }

                Console.WriteLine("Invalid choice. Please enter a number between 1 and 7.");
            }
        }

        /// <summary>
        /// Gets a custom category from user input
        /// </summary>
        /// <returns>The custom category name</returns>
        private string GetCustomCategory()
        {
            while (true)
            {
                Console.Write("Enter custom category name: ");
                var category = Console.ReadLine()?.Trim();

                if (!string.IsNullOrWhiteSpace(category) && category.Length <= 50)
                {
                    return category;
                }

                Console.WriteLine("Invalid category. Please enter a valid category name (max 50 characters).");
            }
        }

        /// <summary>
        /// Asks the user if they want to continue processing transactions
        /// </summary>
        /// <returns>True if user wants to continue, false otherwise</returns>
        private bool AskToContinue()
        {
            while (true)
            {
                Console.Write("Do you want to process another transaction? (y/n): ");
                var input = Console.ReadLine()?.Trim().ToLower();

                switch (input)
                {
                    case "y":
                    case "yes":
                        Console.WriteLine();
                        return true;
                    case "n":
                    case "no":
                        Console.WriteLine();
                        return false;
                    default:
                        Console.WriteLine("Please enter 'y' for yes or 'n' for no.");
                        break;
                }
            }
        }

        /// <summary>
        /// Displays a summary of all processed transactions
        /// </summary>
        /// <param name="account">The account to display information for</param>
        private void DisplayTransactionSummary(SavingsAccount account)
        {
            Console.WriteLine("=== Final Transaction Summary ===");
            Console.WriteLine($"Account: {account.AccountNumber}");
            Console.WriteLine($"Final Balance: ${account.Balance:F2}");
            Console.WriteLine($"Total Transactions Processed: {_transactions.Count}");

            if (_transactions.Count > 0)
            {
                decimal totalSpent = _transactions.Sum(t => t.Amount);
                Console.WriteLine($"Total Amount Spent: ${totalSpent:F2}");

                Console.WriteLine("\nTransaction Details:");
                foreach (var transaction in _transactions)
                {
                    Console.WriteLine($"  ID: {transaction.Id}, Date: {transaction.Date:yyyy-MM-dd HH:mm}, " +
                                    $"Amount: ${transaction.Amount:F2}, Category: {transaction.Category}");
                }
            }
            else
            {
                Console.WriteLine("No transactions were processed.");
            }
            Console.WriteLine();
        }

        // =============== DEPOSIT METHODS ===============

        /// <summary>
        /// Gets the deposit method selection from the user
        /// </summary>
        /// <returns>Integer representing the selected deposit method (0 to cancel)</returns>
        private int GetDepositMethod()
        {
            while (true)
            {
                Console.WriteLine("Select deposit method:");
                Console.WriteLine("1. Mobile Money");
                Console.WriteLine("2. Bank Transfer");
                Console.WriteLine("3. Crypto Wallet");
                Console.WriteLine("0. Cancel");
                Console.Write("Enter your choice (0-3): ");

                var input = Console.ReadLine();
                if (int.TryParse(input, out int choice) && choice >= 0 && choice <= 3)
                {
                    return choice;
                }

                Console.WriteLine("Invalid choice. Please enter a number between 0 and 3.\n");
            }
        }

        /// <summary>
        /// Processes mobile money deposit with network selection
        /// </summary>
        /// <param name="amount">The amount to deposit</param>
        /// <returns>True if deposit was successful</returns>
        private bool ProcessMobileMoneyDeposit(decimal amount)
        {
            Console.WriteLine("=== Mobile Money Deposit ===");

            // Get network selection
            int network = GetMobileMoneyNetwork();
            if (network == 0) return false;

            // Get phone number
            string phoneNumber = GetPhoneNumber();
            if (string.IsNullOrEmpty(phoneNumber)) return false;

            string networkName = network switch
            {
                1 => "MTN",
                2 => "TELECEL",
                3 => "AIRTELTIGO",
                _ => "Unknown"
            };

            Console.WriteLine($"Processing mobile money deposit of ${amount:F2} from {networkName} number {phoneNumber}...");
            Console.WriteLine("Deposit request sent. Please follow the prompt on your phone to complete the transaction.");

            return true;
        }

        /// <summary>
        /// Processes bank deposit
        /// </summary>
        /// <param name="amount">The amount to deposit</param>
        /// <returns>True if deposit was successful</returns>
        private bool ProcessBankDeposit(decimal amount)
        {
            Console.WriteLine("=== Bank Deposit ===");

            // Get bank account details
            var bankDetails = GetBankAccountDetails();
            if (bankDetails == null) return false;

            Console.WriteLine($"Processing bank deposit of ${amount:F2} from {bankDetails.Value.BankName} account {bankDetails.Value.AccountNumber}...");
            Console.WriteLine("Bank transfer initiated successfully.");

            return true;
        }

        /// <summary>
        /// Processes crypto wallet deposit
        /// </summary>
        /// <param name="amount">The amount to deposit</param>
        /// <returns>True if deposit was successful</returns>
        private bool ProcessCryptoDeposit(decimal amount)
        {
            Console.WriteLine("=== Crypto Wallet Deposit ===");

            string walletAddress = GetCryptoWalletAddress("deposit from");
            if (string.IsNullOrEmpty(walletAddress)) return false;

            Console.WriteLine($"Processing crypto deposit of ${amount:F2} from wallet address {walletAddress}...");
            Console.WriteLine("Crypto transfer initiated successfully. Please confirm the transaction in your wallet.");

            return true;
        }

        // =============== WITHDRAWAL METHODS ===============

        /// <summary>
        /// Gets the withdrawal account type selection from the user
        /// </summary>
        /// <returns>Integer representing the selected account type (0 to cancel)</returns>
        private int GetWithdrawalAccountType()
        {
            while (true)
            {
                Console.WriteLine("Select withdrawal account type:");
                Console.WriteLine("1. Mobile Money Account");
                Console.WriteLine("2. Bank Account");
                Console.WriteLine("3. Crypto Wallet");
                Console.WriteLine("0. Cancel");
                Console.Write("Enter your choice (0-3): ");

                var input = Console.ReadLine();
                if (int.TryParse(input, out int choice) && choice >= 0 && choice <= 3)
                {
                    return choice;
                }

                Console.WriteLine("Invalid choice. Please enter a number between 0 and 3.\n");
            }
        }

        /// <summary>
        /// Processes mobile money transfer with enhanced options
        /// </summary>
        /// <returns>Tuple indicating success, amount, and category</returns>
        private (bool success, decimal amount, string category) ProcessMobileMoneyTransfer()
        {
            Console.WriteLine("=== Mobile Money Transfer ===");

            // Get network selection
            int network = GetMobileMoneyNetwork();
            if (network == 0) return (false, 0, "");

            // Get recipient phone number
            string phoneNumber = GetPhoneNumber();
            if (string.IsNullOrEmpty(phoneNumber)) return (false, 0, "");

            // Get amount
            decimal amount = GetTransactionAmount();
            if (amount <= 0) return (false, 0, "");

            // Get category
            string category = GetTransactionCategory();
            if (string.IsNullOrEmpty(category)) return (false, 0, "");

            string networkName = network switch
            {
                1 => "MTN",
                2 => "TELECEL",
                3 => "AIRTELTIGO",
                _ => "Unknown"
            };

            Console.WriteLine($"Mobile money transfer of ${amount:F2} to {networkName} number {phoneNumber} for {category}");

            return (true, amount, category);
        }

        /// <summary>
        /// Processes bank transfer with enhanced options
        /// </summary>
        /// <returns>Tuple indicating success, amount, and category</returns>
        private (bool success, decimal amount, string category) ProcessBankTransfer()
        {
            Console.WriteLine("=== Bank Transfer ===");

            // Get transfer type
            int transferType = GetBankTransferType();
            if (transferType == 0) return (false, 0, "");

            // Get amount
            decimal amount = GetTransactionAmount();
            if (amount <= 0) return (false, 0, "");

            // Get category
            string category = GetTransactionCategory();
            if (string.IsNullOrEmpty(category)) return (false, 0, "");

            bool success = false;

            switch (transferType)
            {
                case 1: // Transfer to another bank
                    success = ProcessBankToBankTransfer(amount);
                    break;
                case 2: // Transfer to mobile money
                    success = ProcessBankToMobileMoneyTransfer(amount);
                    break;
            }

            return (success, amount, category);
        }

        /// <summary>
        /// Processes crypto wallet transfer
        /// </summary>
        /// <returns>Tuple indicating success, amount, and category</returns>
        private (bool success, decimal amount, string category) ProcessCryptoTransfer()
        {
            Console.WriteLine("=== Crypto Wallet Transfer ===");

            // Get operation type
            int operationType = GetCryptoOperationType();
            if (operationType == 0) return (false, 0, "");

            // Get wallet address
            string operation = operationType == 1 ? "send to" : "withdraw from";
            string walletAddress = GetCryptoWalletAddress(operation);
            if (string.IsNullOrEmpty(walletAddress)) return (false, 0, "");

            // Get amount
            decimal amount = GetTransactionAmount();
            if (amount <= 0) return (false, 0, "");

            // Get category
            string category = GetTransactionCategory();
            if (string.IsNullOrEmpty(category)) return (false, 0, "");

            string operationName = operationType == 1 ? "Send" : "Withdraw";
            Console.WriteLine($"{operationName} ${amount:F2} {(operationType == 1 ? "to" : "from")} wallet address {walletAddress} for {category}");

            return (true, amount, category);
        }

        // =============== HELPER METHODS ===============

        /// <summary>
        /// Gets mobile money network selection from user
        /// </summary>
        /// <returns>Integer representing the selected network (0 to cancel)</returns>
        private int GetMobileMoneyNetwork()
        {
            while (true)
            {
                Console.WriteLine("Select mobile money network:");
                Console.WriteLine("1. MTN");
                Console.WriteLine("2. TELECEL");
                Console.WriteLine("3. AIRTELTIGO");
                Console.WriteLine("0. Cancel");
                Console.Write("Enter your choice (0-3): ");

                var input = Console.ReadLine();
                if (int.TryParse(input, out int choice) && choice >= 0 && choice <= 3)
                {
                    return choice;
                }

                Console.WriteLine("Invalid choice. Please enter a number between 0 and 3.\n");
            }
        }

        /// <summary>
        /// Gets a valid 10-digit phone number from user
        /// </summary>
        /// <returns>The phone number or empty string if cancelled</returns>
        private string GetPhoneNumber()
        {
            while (true)
            {
                Console.Write("Enter 10-digit phone number (or 'cancel' to cancel): ");
                var input = Console.ReadLine()?.Trim();

                if (string.IsNullOrEmpty(input) || input.ToLower() == "cancel")
                {
                    return "";
                }

                if (input.Length == 10 && input.All(char.IsDigit))
                {
                    return input;
                }

                Console.WriteLine("Invalid phone number. Please enter exactly 10 digits.");
            }
        }

        /// <summary>
        /// Gets bank account details from user
        /// </summary>
        /// <returns>Bank account details or null if cancelled</returns>
        private (string BankName, string AccountNumber, string AccountHolder)? GetBankAccountDetails()
        {
            // Get bank selection
            int bankChoice = GetBankSelection();
            if (bankChoice == 0) return null;

            string bankName = bankChoice switch
            {
                1 => "GCB",
                2 => "FIRSTBANK",
                3 => "ECOBANK",
                4 => "GTB",
                5 => "BANCASSURANCE",
                6 => "NIB",
                7 => "FIRST NATIONAL BANK",
                8 => "STANBIC BANK",
                _ => "Unknown"
            };

            // Get account number
            string accountNumber = GetBankAccountNumber();
            if (string.IsNullOrEmpty(accountNumber)) return null;

            // Get account holder name
            string accountHolder = GetAccountHolderName();
            if (string.IsNullOrEmpty(accountHolder)) return null;

            return (bankName, accountNumber, accountHolder);
        }

        /// <summary>
        /// Gets bank selection from user
        /// </summary>
        /// <returns>Integer representing the selected bank (0 to cancel)</returns>
        private int GetBankSelection()
        {
            while (true)
            {
                Console.WriteLine("Select bank:");
                Console.WriteLine("1. GCB");
                Console.WriteLine("2. FIRSTBANK");
                Console.WriteLine("3. ECOBANK");
                Console.WriteLine("4. GTB");
                Console.WriteLine("5. BANCASSURANCE");
                Console.WriteLine("6. NIB");
                Console.WriteLine("7. FIRST NATIONAL BANK");
                Console.WriteLine("8. STANBIC BANK");
                Console.WriteLine("0. Cancel");
                Console.Write("Enter your choice (0-8): ");

                var input = Console.ReadLine();
                if (int.TryParse(input, out int choice) && choice >= 0 && choice <= 8)
                {
                    return choice;
                }

                Console.WriteLine("Invalid choice. Please enter a number between 0 and 8.\n");
            }
        }

        /// <summary>
        /// Gets bank account number from user
        /// </summary>
        /// <returns>The account number or empty string if cancelled</returns>
        private string GetBankAccountNumber()
        {
            while (true)
            {
                Console.Write("Enter bank account number (or 'cancel' to cancel): ");
                var input = Console.ReadLine()?.Trim();

                if (string.IsNullOrEmpty(input) || input.ToLower() == "cancel")
                {
                    return "";
                }

                if (input.Length >= 8 && input.All(char.IsDigit))
                {
                    return input;
                }

                Console.WriteLine("Invalid account number. Please enter at least 8 digits.");
            }
        }

        /// <summary>
        /// Gets account holder name from user
        /// </summary>
        /// <returns>The account holder name or empty string if cancelled</returns>
        private string GetAccountHolderName()
        {
            while (true)
            {
                Console.Write("Enter account holder name (or 'cancel' to cancel): ");
                var input = Console.ReadLine()?.Trim();

                if (string.IsNullOrEmpty(input) || input.ToLower() == "cancel")
                {
                    return "";
                }

                if (input.Length >= 2 && input.All(c => char.IsLetter(c) || char.IsWhiteSpace(c)))
                {
                    return input;
                }

                Console.WriteLine("Invalid name. Please enter a valid name (letters and spaces only, minimum 2 characters).");
            }
        }

        /// <summary>
        /// Gets crypto wallet address from user
        /// </summary>
        /// <param name="operation">The operation description (e.g., "send to", "withdraw from")</param>
        /// <returns>The wallet address or empty string if cancelled</returns>
        private string GetCryptoWalletAddress(string operation)
        {
            while (true)
            {
                Console.Write($"Enter crypto wallet address to {operation} (or 'cancel' to cancel): ");
                var input = Console.ReadLine()?.Trim();

                if (string.IsNullOrEmpty(input) || input.ToLower() == "cancel")
                {
                    return "";
                }

                if (input.Length >= 26 && input.Length <= 62) // Typical crypto address length range
                {
                    return input;
                }

                Console.WriteLine("Invalid wallet address. Please enter a valid crypto wallet address (26-62 characters).");
            }
        }

        /// <summary>
        /// Gets bank transfer type selection from user
        /// </summary>
        /// <returns>Integer representing the selected transfer type (0 to cancel)</returns>
        private int GetBankTransferType()
        {
            while (true)
            {
                Console.WriteLine("Select bank transfer type:");
                Console.WriteLine("1. Transfer to another bank");
                Console.WriteLine("2. Transfer to mobile money");
                Console.WriteLine("0. Cancel");
                Console.Write("Enter your choice (0-2): ");

                var input = Console.ReadLine();
                if (int.TryParse(input, out int choice) && choice >= 0 && choice <= 2)
                {
                    return choice;
                }

                Console.WriteLine("Invalid choice. Please enter a number between 0 and 2.\n");
            }
        }

        /// <summary>
        /// Processes bank to bank transfer
        /// </summary>
        /// <param name="amount">The transfer amount</param>
        /// <returns>True if transfer was successful</returns>
        private bool ProcessBankToBankTransfer(decimal amount)
        {
            var bankDetails = GetBankAccountDetails();
            if (bankDetails == null) return false;

            Console.WriteLine($"Processing bank transfer of ${amount:F2} to {bankDetails.Value.BankName} account {bankDetails.Value.AccountNumber} ({bankDetails.Value.AccountHolder})");
            return true;
        }

        /// <summary>
        /// Processes bank to mobile money transfer
        /// </summary>
        /// <param name="amount">The transfer amount</param>
        /// <returns>True if transfer was successful</returns>
        private bool ProcessBankToMobileMoneyTransfer(decimal amount)
        {
            int network = GetMobileMoneyNetwork();
            if (network == 0) return false;

            string phoneNumber = GetPhoneNumber();
            if (string.IsNullOrEmpty(phoneNumber)) return false;

            string networkName = network switch
            {
                1 => "MTN",
                2 => "TELECEL",
                3 => "AIRTELTIGO",
                _ => "Unknown"
            };

            Console.WriteLine($"Processing bank to mobile money transfer of ${amount:F2} to {networkName} number {phoneNumber}");
            return true;
        }

        /// <summary>
        /// Gets crypto operation type selection from user
        /// </summary>
        /// <returns>Integer representing the selected operation (0 to cancel, 1 for send, 2 for withdraw)</returns>
        private int GetCryptoOperationType()
        {
            while (true)
            {
                Console.WriteLine("Select crypto operation:");
                Console.WriteLine("1. Send to wallet address");
                Console.WriteLine("2. Withdraw from wallet address");
                Console.WriteLine("0. Cancel");
                Console.Write("Enter your choice (0-2): ");

                var input = Console.ReadLine();
                if (int.TryParse(input, out int choice) && choice >= 0 && choice <= 2)
                {
                    return choice;
                }

                Console.WriteLine("Invalid choice. Please enter a number between 0 and 2.\n");
            }
        }
    }
}
