   M S B A M L   `   `   `  ����B  >WpfApp1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:WpfApp1WpfApp1  5   	   L HCoursesManagerLib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullP:clr-namespace:CoursesManagerLib;assembly=CoursesManagerLibCoursesManagerLib 5   	       WpfApp1.MainWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:WpfApp1     5   	   XCoursesManagerLib:clr-namespace:CoursesManagerLib;assembly=CoursesManagerLib     5   	     � Title$  
MainWindow��5	   	   $	��800��6   $
��1200��6)    � 
ResizeMode$ NoResize=�66   .��5
      �� .+�5   
   � -   5         �Name$ 	GridAdmin��5      $y�Visible=�6"   ���	#FFD67979$	��780��6O   $
��1190��6\   .+�5      �� -   5         ��Name$ 	ShowPanel��5       �� 
IsReadOnly . $
��Left=�6<   $	��613��6W   $��28.2,66.2,0,0q�6d    �� TextWrapping$
 Wrap=�6{   $	��Top=�6�   $	��769��6�   " �� VerticalScrollBarVisibility$ Visible=�6�    �� 	UndoLimit$ 100000��6�   $��20(�6�   $��Times New Roman)�6�   {�.  �� IsReadOnlyCaretVisible . ���	#FFEAD5D56F  �� 5      -   5        	 ��Name$	 
ShowGroups��5      $��857,70,49,643q�6)   $��20(�6@   $��Show Groups��6N   6d   �� 5      -   5        $	 ShowClients��5      $��857,160,49,545q�6*   $��20(�6B   $��Show Clients��6P   6g   �� 5      -   5        $	 
ShowClaims��5      $��857,250,49,448q�6)   $��20(�6A   $��Show Claims��6O   6e   �� 5      -   5        $	 ButtonAdmission��5      $��	Admission��6.   $
��Left=�6B   $��63��6]   $��899,352,0,0q�6i   $	��Top=�6~   $	��190��6�   $��20(�6�   6�   �� 5      -   5        $	 ButtonID��5      $��ShowGroupsId��6'   $
��Left=�6>   $��63��6Y   $��870,464,0,0q�6e   $	��Top=�6z   $	��247��6�   $��20(�6�   6�   �� 5      -   5        $	 ButtonClientWindow��5      $��ClientWindow��61   $
��Left=�6H   $��64��6c   $��899,680,0,0q�6o   $	��Top=�6�   $	��190��6�   $��20(�6�   ���	#FF6D8EDA6�   �� 5      -	   5        $ TextBoxIDSearch��5      $
��Left=�6/   $��45��6J   $��870,547,0,0q�6V   $
 Wrap=�6k   $	��Top=�6   $	��247��6�   ���	#FFBAB8E5$��20(�6�   �� 5      -
   5        $	 ButtonSetAttendance��5      $��
28,700,0,0q�62   $��SetAttendance��6F   $
��Left=�6^   $��63��6y   $	��Top=�6�   $	��190��6�   $��20(�6�   
�������6�   c� 5      -   5        
 c�Name$
 toolBar��5      ���	#FFF67979$
��Left=�6>   $��61��6Y   $	��Top=�6e   $
��1190��6}   .@�5      �� -   5        $	 
SaveButton��5      $��20(�6-   $
��Save��6;   $��56��6J   $	��Top=�6V   $	��150��6n   6z   �� 5      -   5        $	 
LoadButton��5      $��20(�6-   $
��Load��6;   $��56��6J   $	��Top=�6V   $	��150��6n   6z   o� 5      $��50��6   $	��Top=�6#   $	��200��6;   $��	550,5,0,0q�6G   ���	#FFF67979.@�5      m� $��Instruction��6   ���	#FFF67979$��22(�6K   .@�5      m� -   5         m�Name$ AdminEngInstruction��5   #   $��English��6@   6Q   m� 5      -   5        $ AdminRusInstruction��5   #   $��Russian��6@   6Q   �� 5       5!      5"      5#      �� 5$      -   5        $	 ButtonGetAttendance��5$      $��277,700,0,0q�63   $��GetAttendance��6H   $
��Left=�6`   $��63��6{   $	��Top=�6�   $	��190��6�   $��20(�6�   
�������6�   5%      5&      5'      