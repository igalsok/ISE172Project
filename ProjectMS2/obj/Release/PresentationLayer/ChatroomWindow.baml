   M S B A M L   `   `   `  ���� E  AProjectMS2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullK*clr-namespace:ProjectMS2.PresentationLayerProjectMS2.PresentationLayer  C&clr-namespace:ProjectMS2.BusinessLayerProjectMS2.BusinessLayer  1    +ProjectMS2.PresentationLayer.ChatroomWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     <local*clr-namespace:ProjectMS2.PresentationLayer     @BusinessLayer&clr-namespace:ProjectMS2.BusinessLayer     -     � WindowStartupLocation$  CenterScreen=� � Title$ 	ChatRo0om��$��578.5��$	��900������  �� EndPoint$ 0.5,1&� �� 
StartPoint$ 0.5,0&�.���  � Color$ 	#FF4DC0AB�� � Offset$ 0.484Q�� $ 	#FF257570��$ 0.821Q� J� Effect 3  -System.Windows.Media.Effects.DropShadowEffect  .��� $��0,2,-14.4,0.8q� � ColumnDefinitions �� �� $��0* �.+��� -    ��Name$ Logout��$��Logout��$��20(�$
��Left=�$	��Top=�$	��129��$��711,390,0,0q�$��50��$��Monotype Corsiva)��� -   	 ��Name$	 txtBox_sendMsg��$
��Auto=�$
��Left=�$��60��
 �� TextWrapping$

 Wrap=�$	��Top=�$	��600��$��
10,475,0,0q��� -    ��Name$ lst_Display��
�������$
��Left=�$	��440��$	��Top=�$	��645��$��5,10,0,0q����� *DisplayList+ �� UpdateSourceTrigger$ PropertyChanged=�����  �� 
TargetType" ��.�-   �� $��Sort Messages by:��$
��Left=�$	��Top=�$��
724,54,0,0q� J� RenderTransformOrigin$ 0.525,1.179&�$��26��$	��106���� -   $ btn_send��$
��Send��$��18(�$
��Left=�$��613,475,0,0q�$	��Top=�$��92��$��60��$��Monotype Corsiva)��� $��Filter Messages by:��$
��Left=�$	��Top=�$��707,225,0,0q�$ 0.525,1.179&�$	��116��$��26���� -   $	 txtBox_IdFilter��$y�visible=�$
��Left=�$��23��$��667,255,0,0q�$

 Wrap=�$	��Top=�$��92���� -   $	 txtBox_uNameFilter��$y�Hidden=�$
��Left=�$��769,255,0,0q�$

 Wrap=�$	��Top=�$��92��$��23���� -	    ��Name$ lbl_gId��$��Group ID��$
��Left=�$��677,280,0,0q�$	��Top=�$��25��$��84���� -
   $ 	lbl_uName��$��UserName��$y�Hidden=�$
��Left=�$��775,280,0,0q�$	��Top=�$��26��$��66���� -   $ 	filterbtn��$��Reset Fields��$
��Left=�$��790,310,0,0q�$	��Top=�$��91��$��40��$��Monotype Corsiva)�$��16(��� -   $ lbl_Con��
��� � �$��	Connected��$
��Left=�$��453,398,0,0q�$	��Top=�$��31��$	��100��$
��Bold"�$y�Hidden=��� $��668,93,-239.8,429q�$��2��._�� $��1,-5,0,-6.4q�$
��Left=�$	��240��.+�	� -    	�Name$ chk_as��$��	Ascending��$
��Left=�$	��Top=�$��15��$��74��	� -   $ chk_des��$��
Descending��$��119,0,-21,0q�$	��Top=�$��17���� $��675,130,-245.8,392q�$��2��._�� .+�	� -   $ chk_time��$
��Time��$
��Left=�$	��Top=�$��25��$��45��$��
-5,-15,0,0q�	� -   $ chk_nickname��$��Nickname��$
��Left=�$��55,-15,0,5.2q�$��75��	� -   $ chk_gId��$��Group ID��$
��Left=�$��75��$��130,-15,-5,-4.8q�$ 
0.475,0.84&��� $��Flter my messages:��$
��Left=�$��656,320,0,0q�$	��Top=�$ -0.391,-0.016&�$��30��$	��120���� -    ��Name$ chk_myFilter��$
��Left=�$��770,325,0,0q�$	��Top=�$��15��$��16���� $��version 2.0.1��$
��Left=�$��810,509,0,0q�$��26��$	��Top=��� -   $ btn_info��$
��Info��$
��Left=�$��810,475,0,0q�$	��Top=�$��75��$��20(�$��Monotype Corsiva)�$
��Bold"�$��Italic$��� -   $ chk_autoScroll��$��
AutoScroll��$
��Left=�$��570,455,0,0q�$	��Top=�