   M S B A M L   `   `   `  ����?  ;Akip, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:AkipAkip  5          Akip.JobPage   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      $localclr-namespace:Akip     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5        N� Title$  JobPage��5       � 	Resources 5
         Akip.JobPageViewModel 	  jobVM��%   &	          5   
   6!   5       � DataContext 5      �� 5   
    �� Source# [  6   .��5      �  � RowDefinitions 5   
   �� 5      $��20 �6   �� 5      $
��auto �6   �� 5      $��* �6   .+�5   
   �� $��1��6   .+�5      ��   
2TitleText#��[ 6   $'��!Контроль нагрузки��6<   �� 5      ���#DCDEDD6   5      � 5   
   $��2��6   $��
10,20,10,0q�6   .+�5      ]� .+�5       �� $	��Top=�6     HelpTitleText#��[ 61   $'��!Панель управления��6X   �� 5!      $��
10,1.5,0,0q�6   $	��Top=�5"      ���	#FFA8A8A8  LinkText#��[ 5$      ZXПанель управления потоками программы испытаний5&      ]� 5'      $��	10,10,0,0q�6   $
��Left=�5(      $	��Top=�5)      $	��Top=�5*      .+�5+      �� $	��150��6   $��30��5,      $��	0,0,100,0q�5-      $/��)Инициализация потоков��5/      $
��Left=�50        MainButtonStyle#��[ 51      ��5.      �� *ProcessesInitialization+ �� UpdateSourceTrigger$ PropertyChanged=� �� Mode$ TwoWay=�51      �� 52      $	��150��6   $��30��53      $��0,0,5,0q�54      $.��(Запуск всех процессов��56      $
��Left=�57      #��[ 58      ��55      �� *StartAllLoadProcesses+$ PropertyChanged=�$ TwoWay=�58      �� 59      $	��170��6   $��30��5:      $��0,0,10,0q�5;      $4��.Остановка всех процессов��5=      $
��Left=�5>      #��[ 5?      ��5<      �� *StopAllLoadProcesses+$ PropertyChanged=�$ TwoWay=�5?      5@      �� 5B      $��0,10,0,0q�6   ��61   �� *ProcessControlColleciton+$ PropertyChanged=���5C      �� 5D      .R�5E      �  � Orientation$ 
Horizontal=�6(   5F      ��5H      �� 5I      .R�5J         Akip.WorkloadControl  $
��5,10q�64   5K      5M      5O      5P      5Q      5S      