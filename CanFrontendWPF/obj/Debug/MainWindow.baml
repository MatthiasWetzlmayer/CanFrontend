   M S B A M L   `   `   `  ����I  ECanFrontendWPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:CanFrontendWPFCanFrontendWPF  5   	       CanFrontendWPF.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   .localclr-namespace:CanFrontendWPF     5   	      MaterialDesignBody#���   5   	   $��Regular"�5	   	   $��13(�5
   	   &   System.Windows.Media.TextOptions    TextFormattingMode$  Ideal=�5   	      TextRenderingMode$
 Auto=�5   	     MaterialDesignPaper#���  5   	     MaterialDesignFont#���  5   	    � Title$ 
MainWindow��5   	   $	��450��6   $	��800��6)   .��5   
   �  � RowDefinitions 5      �� 5      $��* �6    �� 5      $��* �6    �� 5      $��4* �6    �� 5      $��* �6     � ColumnDefinitions 5      �� 5      $	��23* �6#   61   �� 5      $
��404* �6#   �� 5      $	��71* �6#   61   �� 5       $	��75* �6#   61   �� 5!      $	��71* �6#   61   .+�5#      �� -   5         ��Name$ DiscoverButton��5#      $��Discover Devices��6-   $
��Left=�6e   $��	127,4,0,3q�6�   $
��10,0q�6�   $��1��6�   �� 5$      $��4��6   $��3��6*   $��
0,39,71,39q�6>   .+�5%      �� $��Available Devices��6   $
��Bold"�65   $
��10,0q�6G   �� 5&      -   5         ��Name$ AvailableDevicesListBox��5&      $��10q�6;    �� SelectionMode$ Single=�6G   ��5'      �� 5(       �� 
TargetType" ��6    .�5)      �� 	 �� Property	 � ��
 �� Value

 �   �6;   5*      5,      5-      �� 5.      $��3��6   $��3��6&   $��0,0,71,0q�6:   .+�5/      �� $��Connected Devices��6   $
��Bold"�65   $
��10,0q�6G   �� 50      -   5        $ ConnectedDevicesListBox��50      $��10q�6;   $ Single=�6G   ��51      �� 52      " ��6    .�53      �� 	 � ��

 � � �6;   54      56      57      �� 59      -   5        $ ConnectButton��59      $��2��6,   $��Connect��6<   $��Center=�6j   $��1��6�   $��
0,10,0,154q�6�   $
��10,0q�6�   $��2��6�   �� 5:      -   5        $ DisconnectButton��5:      $��3��6/   $��
Disconnect��6?   $��Center=�6s   $��3��6�   $��	0,10,0,-3q�6�   $
��10,0q�6�   �� 5;      -   5        $ ReadData��5;      $��4��6'   $��	Read Data��67   $��Center=�6h   $��3��6�   $��	0,10,0,-3q�6�   $
��10,0q�6�   5=      5?      