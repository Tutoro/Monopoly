   M S B A M L   `   `   `  ����C  ?Monopoly, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:MonopolyMonopoly  5   	       Monopoly.WindowScambio   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   (localclr-namespace:Monopoly     5   	   -   5          �Name$  wScambio��5   	    � Title$ Finestra di Trade��5   	   $��348.738��6#   $��533.204��64   .��5	      � $��0,0,0,0q�6    � ColumnDefinitions 5
   
   �� 5      .+�5   
   �� -   5         ��Name$ nomeGiocatore��5      $��nomeGiocatore��6'   $
��Left=�6?   $��50,8,0,0q�6Z   $	��Top=�6l   $��35��6�   $	��154��6�   $��18(�6�   �� 5   
   
���   �$
��Left=�6!   $	��270��6<   $��	262,8,0,0q�6I   
���   �$	��Top=�6k   $��2��6�    J� Opacity$
 0.45Q�6�   �� 5   
   -   5        $
 txt1��5      $��Proprietà scambiabili:��6   $
��Left=�6?   $��7,52,0,0q�6Z   $	��Top=�6l   $��10(�6�   �� 5   
   -   5         ��Name$ listaProprietaUno��5      $
��Left=�6-   $��61��6H   $��	7,144,0,0q�6T   $	��Top=�6g   $	��241��6   6�   �� 5   
   -   5        $
 txt3��5      $��Proprietà scelte:��6   $
��Left=�6:   $��	7,121,0,0q�6U   $	��Top=�6h   $��10(�6�   �� 5   
   -   5         ��Name$ addUno��5      $��+��6!   $
��Left=�6-   $��
219,80,0,0q�6H   $	��Top=�6\   $��29��6t   $��29��6   $��18(�6�   6�   �� 5   
   -   5         ��Name$ vaultUno��5      $
��Left=�6$   $��23��6?   $��	7,240,0,0q�6K    �� TextWrapping$
 Wrap=�6^   $	��Top=�6r   $	��104��6�   $��0��6�   �� 5   
   -   5        $
 txt5��5      $��Valuta da scambiare:��6   $
��Left=�6=   $��	7,217,0,0q�6X   $	��Top=�6k   $��10(�6�   �� 5   
   -	   5        $ sendUno��5      $��Invia contratto��6"   $
��Left=�6<   $��146,240,0,0q�6W   $	��Top=�6l   $	��102��6�   $��23��6�   6�   �� 5   
   -
   5        $
 txt2��5      $��Proprietà scambiabili:��6   $
��Left=�6?   $��
276,52,0,0q�6Z   $	��Top=�6n   $��10(�6�   �� 5   
   -   5        $ listaProprietaDue��5      $
��Left=�6-   $��61��6H   $��276,144,0,0q�6T   $	��Top=�6i   $	��241��6�   6�   �� 5   
   -   5        $
 txt4��5      $��Proprietà scelte:��6   $
��Left=�6:   $��276,121,0,0q�6U   $	��Top=�6j   $��10(�6�   �� 5   
   -   5        $ addDue��5      $��+��6!   $
��Left=�6-   $��
488,80,0,0q�6H   $	��Top=�6\   $��29��6t   $��29��6   $��18(�6�   6�   �� 5   
   -   5        $ vaultDue��5      $
��Left=�6$   $��23��6?   $��276,240,0,0q�6K   $
 Wrap=�6`   $	��Top=�6t   $	��104��6�   $��0��6�   �� 5   
   -   5        $
 txt6��5      $��Valuta da scambiare:��6   $
��Left=�6=   $��276,217,0,0q�6X   $	��Top=�6m   $��10(�6�   �� 5   
   -   5        $ sendDue��5      $��Invia contratto��6"   $
��Left=�6<   $��415,240,0,0q�6W   $	��Top=�6l   $	��102��6�   $��23��6�   6�   �� 5   
   -   5        $ stopAll��5      $��Annulla contratto��6"   $
��Left=�6>   $��214,283,0,0q�6Y   $	��Top=�6n   $��98��6�   $��25��6�   6�   �� 5   
   -   5        	 ��Name$	 listaGiocatori��5      $
��Left=�6+   $��
341,16,0,0q�6F   $	��Top=�6Z   $	��120��6r   6~   �� 5   
   -   5        $	 proprietaDue��5      $
��Left=�6)   $��
276,80,0,0q�6D   $	��Top=�6X   $	��207��6p   $��29��6|   �� 5    
   -   5        $	 proprietaUno��5       $
��Left=�6)   $��7,80,0,0q�6D   $	��Top=�6V   $	��207��6n   $��29��6z   5"      5#      