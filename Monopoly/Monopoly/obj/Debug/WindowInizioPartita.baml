   M S B A M L   `   `   `  ����C  ?Monopoly, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:MonopolyMonopoly  5   	   "    Monopoly.WindowInizioPartita   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   (localclr-namespace:Monopoly     5   	     � Title$  WindowInizioPartita��5   	   $	��306��6%   $	��886��62    � 
ResizeMode$ NoResize=�6>   ��5	      �� 5
   
   .��5      � ��5   
   �� 5       �� ImageSource$  Immagini/Monopoly_logo.png��6   .+�5   
   �� -   5         ��Name$ TextBox_NumeroGiocatori��5      $
��Left=�63   $��25��6N   $��
575,45,0,0q�6Z    �� TextWrapping$
 Wrap=�6n   $	��Top=�6�   $	��253��6�   ���	#FFE2001A$��1q�6�   �� 5   
   $$��Decidi il numero di giocatori:��6   $
��Left=�69   $��
575,10,0,0q�6T   $	��Top=�6h   $��30��6�   $	��253��6�   ���	#FFE2001A$��2q�6�   ��6�   Z� 6�   �� 5   
   -   5         ��Name$ label��5      $+��%Decidi Il numero di turni da giocare:��6   $
��Left=�6O   $��	10,10,0,0q�6j   $	��Top=�6}   ���	#FFE2001A$��2q�6�   $	��331��6�   ��6�   Z� 6�   �� 5   
   -   5         ��Name$ checkBox_Infiniti��5      $#��Desidero avere turni infiniti��6.   $
��Left=�6V   $��	10,40,0,0q�6q   $	��Top=�6�   6�   �� 5   
   -   5        $ checkBox_Finiti��5      $*��$Desidero decidere il numero di turni��6,   $
��Left=�6[   $��	10,55,0,0q�6v   $	��Top=�6�    J� RenderTransformOrigin$ 0.695,-0.083&�6�   �� 5   
   -   5         ��Name$ canvas_turni��5      $
��Left=�6'   $��25��6B   $��
241,45,0,0q�6N   $	��Top=�6b   $	��100��6z   $y�	Collapsed=�6�   .+�5      �� -   5        $ 	lbl_turni��5      $��	N. turni:��6'   $
��Left=�6;   $	��Top=�6V   $��54��6n   �� 5      -   5        $ textBox_ScegliTurni��5      $
��Left=�63   $��25��6N   $
 Wrap=�6Z   $	��Top=�6n   $��50��6�   	 �� Left$	 50��6�   $ 0.5,0.5&�6�   ���	#FFE2001A6�   5      �� 5   
   -   5        $ Button_Conferma��5      $��INIZIA!��6)   $��Center=�6;   $��Center=�6_   $
��Left=�6�   $��354,222,0,0q�6�   $	��Top=�6�   $	��169��6�   $��36��6�   ���	#FFE41F1F���	#FFE61B1B
�������6>  5      5      