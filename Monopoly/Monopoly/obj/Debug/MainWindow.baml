   M S B A M L   `   `   `  ����C  ?Monopoly, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:MonopolyMonopoly  5   	       Monopoly.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   (localclr-namespace:Monopoly     5   	   -   5          � Title$  
MainWindow��5   	   $	��896��6   $
��1102��6)    � 
ResizeMode$ NoResize=�66   ���	#FFCDE7CE.��5	      � -   5         �Name$ Griglia_Principale��5	      .+�5
   
   �� 
���   �$��1q�6%   $
��Left=�69   $��65��6T   $��806,784,0,0q�6`   $	��Top=�6u   $	��272��6�   ��5      �� 5       �� Color# � ��6"   5      �� 5   
   -   5         ��Name$ Image_Campo��5      $
��Left=�6%   $	��800��6@   $	��Top=�6M   $	��800��6e   $%��Immagini/Tabellone_monopoli.jpg��6q   �� 5   
   -   5         ��Name$ Button_Azioni��5      $��Azioni��6(   $
��Left=�69   $��	0,805,0,0q�6T   $	��Top=�6g   $	��260��6   $��44��6�   
���   ���5      �� 5      # � ��6"   5      �� 5   
   -   5        $ Button_Scambia��5      $��Scambia��6)   $
��Left=�6;   $��266,805,0,0q�6V   $	��Top=�6k   $	��270��6�   $��44��6�   
���   ���5      �� 5      # � ��6"   5      �� 5   
   -   5        $ Button_Passa��5      $��Passa��6'   $
��Left=�67   $��540,805,0,0q�6R   $	��Top=�6g   $	��260��6   $��44��6�   
���   ���5      �� 5      # � ��6"   5      o� 5   
   -   5         o�Name$ Menu_Azioni��5      $
��Left=�6$   $��66��6?   $��	0,734,0,0q�6K   $	��Top=�6^   $	��146��6v   $y�	Collapsed=�6�   
���   �$��2q�6�   ��5       �� 5!      # � ��6"   .@�5#      �� -   5        $ Button_CompraStrutture��5#      $��Compra/Vendi Strutture��65   $	��130��6n   �� 5$      -	   5        $ Button_CompraProprieta��5$      $��Compra Proprietà��65   $	��130��6h   �� 5%      -
   5        $ Button_VendiProprieta��5%      $��Ipoteca Proprietà��64   $	��130��6P   6\   5&      �� 5'   
   -   5         ��Name$ TextBlock_SoldiG1��5'      $
��Left=�6/   $��806,784,0,0q�6J   $
��Wrap=�6_   $��	TextBlock��6s   $	��Top=�6�    J� RenderTransformOrigin$ -0.129,-0.729&�6�   $	��272��6�   
���   �6�   �� 5(   
   -   5        $ TextBlock_SoldiG2��5(      $
��Left=�6/   $��806,800,0,0q�6J   $
��Wrap=�6_   $��	TextBlock��6s   $	��Top=�6�   $ -0.129,-0.729&�6�   $	��272��6�   
�������6�   �� 5)   
   -   5        $ TextBlock_SoldiG3��5)      $
��Left=�6/   $��806,816,0,0q�6J   $
��Wrap=�6_   $��	TextBlock��6s   $	��Top=�6�   $ -0.129,-0.729&�6�   $	��272��6�   
������6�   �� 5*   
   -   5        $ TextBlock_SoldiG4��5*      $
��Left=�6/   $��806,832,0,0q�6J   $
��Wrap=�6_   $��	TextBlock��6s   $	��Top=�6�   $ -0.129,-0.729&�6�   $	��272��6�   
���<��6�   �� 5+   
   
���   �$��1q�6%   $
��Left=�69   $	��800��6T   $	��Top=�6a   $	��800��6y   �� 5,   
   -   5        	 ��Name$ 	 StackPanel_ProprietaUtente��5,      $
��Left=�69   $	��779��6T   $��	806,0,0,0q�6a   $	��Top=�6t   $	��272��6�   ��5-      �� 5.      # � ��6"   50      �� 51   
   
���   �$��1q�6%   $
��Left=�69   $	��779��6T   $��	806,0,0,0q�6a   $	��Top=�6t   $	��272��6�   52      53      