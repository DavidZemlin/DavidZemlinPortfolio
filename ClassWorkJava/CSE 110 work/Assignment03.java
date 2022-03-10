// Class       : CES 110 / Online
// Assignment  : 03
// Author      : David Zemlin 1223802827
// Description : Determines if a stock is worth buying or selling

import java.util.Scanner;

public class Assignment03 
{
	public static void main(String[] args)
	{
        // declare and instantiate a Scanner
		Scanner scan = new Scanner(System.in);

        // declare and initialize variables
		final int TRANSACTION_FEE = 10;
		int currentShares = 0;
		int purchasePrice = 0;
		int marketPrice = 0;
		int availableFunds = 0;
		int sharesToTrade = 0;
		boolean buy = false;
		boolean sell = false;
		
        // prompt for and collect inputs
		System.out.println("Current Shares  : ");
		currentShares = scan.nextInt();
		System.out.println("Purchase Price  : ");
		purchasePrice = scan.nextInt();
		System.out.println("Market Price    : ");
		marketPrice = scan.nextInt();
		System.out.println("Available Funds : ");
		availableFunds = scan.nextInt();

        // compute required values
		// consider buying shares if market price is low
		if (marketPrice < purchasePrice)
		{
			// how many could we buy?
			int sharesToBuy = (availableFunds - TRANSACTION_FEE) / marketPrice;
			if (sharesToBuy > 0)
			{
				// what is a the potential value of the purchase?
				int perShareValue = purchasePrice - marketPrice;
				int totalValue = perShareValue * sharesToBuy;
				
				// if the value is greater than the transaction fee, buy the stock
				if (totalValue > TRANSACTION_FEE)
				{
					buy = true;
					sharesToTrade = sharesToBuy;
				}
			}
		}
		
		// consider selling shares if market price is high
		else if (purchasePrice < marketPrice)
		{
			// what is a the potential value of the sale?
			int perShareValue = marketPrice - purchasePrice;
			int totalValue = perShareValue * currentShares;
			
			// if the value is greater than the transaction fee, sell the stock
			if (totalValue > TRANSACTION_FEE)
			{
				sell = true;
				sharesToTrade = currentShares;
			}
		}

        // display required outputs
		if (buy)
		{
			System.out.println("Buy " + sharesToTrade + " shares");
		}
		else if (sell)
		{
			System.out.println("Sell " + sharesToTrade + " shares");
		}
		else
		{
			System.out.println("Hold shares");
		}
		
		//Close input scanner to prevent resource leaks.
		scan.close();
	}
}
