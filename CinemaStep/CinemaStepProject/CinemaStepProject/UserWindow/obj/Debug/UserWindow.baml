   M S B A M L   `   `   `  ����c  _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35qPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.InteractivitySystem.Windows.Interactivity  5   	   E AUserWindow, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:UserWindow
UserWindow 5   	       UserWindow.UserWindowClass   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ^iPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.Interactivity     5   	   *localclr-namespace:UserWindow     5   	   $	��700��5
   (   $	��500��68     � 
ResizeMode$  NoResize=�6D    � WindowStyle$
 None=�6Z      WindowStyle#���   5   	    � WindowStartupLocation$ CenterScreen=�6/    � SizeToContent$ Height=�6T    � Title 5
   	   �� *Phone_Number+ � 	Resources 5       �� 
TargetType �� Property  WindowBackgroundBrush �� Value  DefaultMarginStyle  TextblockWeatherValueStyle  TextblockWeatherOptionStyle  ListBoxStyle	 � Orientation  TextblockColorStyle* $UserWindow.UrlToImageSourceConverter  UrlToImageSourceConverter��%�  &	        &	 F     &	 �     &	 �    &	 ~    &	 8    &	 �    �� 5   
   " �6$   .�5      ��  � ��# �  6+   5      �� 5   
   " ��6+   .�5      ��  � ��$ 5q�6'   5      �� 5   
   " ��63   .�5      ��  � ��$ Center=�62   �� 5       � ��$ 30(�6)   �� 5       � ��$
 Bold"�6+   �� 5       � ��$ Arial)�6+   �� 5       � ��$ Right=�64   �� 5       � ��$ 0 0 50 0q�6'   �� 5       � ��
 �����6+   5      �� 5!   
   " ��64   .�5"      ��  � ��$ 	Underline{�60   �� 5#       � ��$ 20(�6)   �� 5$       � ��$ 20q�6'   �� 5%       � ��
 �����6+   5&      �� 5(   
   " ��6%   .�5)      ��  � �� 5*      �� 5+      .R�5,      � $	 
Horizontal=�6$   $��Center=�5-      $��Center=�5.      5/      51      52      �� 54   
   " ��6,   .�55      ��  � ��
 �����6+   �� 56       � ��$ 5q�6'   57        59   
   6*   5:      .��5<      � .+�5>   
   �� .@�5@      �� $��Order Ticket��6   .��5A      �� .+�5C      �� 
 �� Orientation$
 
Horizontal=�6!   $��Center=�6:   .+�5E      �� $��10 0 0 0q�6%   .+�5F      �� $��Choose your city: ��6(   #��[ 6B   �� 5G      $	��100��6'   $
��Left=�63   $��5q�6N   ��6Y   �� *Cities+ �� SelectedItem 6x   �� *SelectedCity+.   (System.Windows.Interactivity.Interaction   Triggers 5H   "   /   )System.Windows.Interactivity.EventTrigger  5I   &      	EventName$ SelectionChanged��65   .   (System.Windows.Interactivity.TriggerBase   Actions. 5J   *   6   0System.Windows.Interactivity.InvokeCommandAction     Command 6@   �� *CityChangedCommand+5K   '   5M      5N      �� 5P      $��70 0 0 0q�6%   .+�5Q      �� $��Choose your cinema: ��6(   #��[ 6D   �� 5R      $	��140��6'   $
��Left=�63   $��5q�6N   ��5S   '   �� *	Cinemas+ �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=� 5T   '   �� *SelectedCinema+$ PropertyChanged=�$ TwoWay=�5U      5V      �� 5X      $
 
Horizontal=�6!   $��Center=�6:   $��10q�6W   .+�5Y      �� $#��Choose the category of film: ��6$   #��[ 6I   �� 5Z      $	��100��6#   $��5q�6/   ��6:   �� *
Categories+ 6]   �� *SelectedCategory+ 5[        5\   "   $ SelectionChanged��61   . 5]   &      6<   �� *CategoryChangedCommand+5^   #   5`      5a      �� 5c      $��10q�6   $ �� IsSynchronizedWithCurrentItem$
 TrueY�5e      #���  5g      ��5d      �� *Films+ 5f      �� *SelectedFilm+$ PropertyChanged=�$ TwoWay=���5i      �� 5j      .R�5k   "   �� .+�5l   &   �� $	��230��6   $	��230��6�   $��Uniform=�6�   ��6,   �� *	image_url+ �� 	Converter# [ 6�   �� 5m   &   #��[ 60   $
��Bold"�6t   $��Center=�6�   ��6]   �� *title+6�   5n   #   5o       5r        5s      $ SelectionChanged��6-   . 5t   "      68   �� *FilmChangedCommand+5u      5x      ]� 5{      $��5q�6    .+�5|      �� $��Available Sessions: ��6$   #��[ 6@   $
��Left=�6n   �� 5}      $��Take your place: ��6$   #��[ 6=   $��Right=�6j   5~      ]� 5�      $��5q�6    .+�5�      �� $	��300��5�   "   $
��Left=�5�   "   $��5 0 0 0q�5�   "   ��5�   "   �� *
Sessions+$ PropertyChanged=�$ TwoWay=� 5�   "   �� *SelectedSession+$ PropertyChanged=�$ TwoWay=���5�      �� 5�   "   .R�5�   &   �� $
 
Horizontal=�61   .+�5�   *   �� $��Hall: ��64   �� 5�   *   ��64   �� *	id_hall+�� 5�   *   $�� Date: ��64   �� 5�   *   ��64   ��  �� Path$
 date� �� StringFormat$ {0:MM/dd/yyyy a\t h:mm }��5�   '   5�   #    5�        5�   "   $ SelectionChanged��61   . 5�   &      6<   �� *SessionChangedCommand+5�   #   5�      �� 5�      $��Right=�6#   $	��100��5�   #   ��5�   #   �� *FreePlacesForSession+ 5�   #   �� *SelectedPlace+$ PropertyChanged=�5�      �� 5�      $��To Order A Ticket��6   $��20q�69   $��Center=�6E   $��50��6�   ��6b   �� *ToOrderCommand+6�   �� 5�      $��Center=�6a   #��[ 6~   ��6    �� *
StatusText+$ PropertyChanged=�6~   �� 5�      $'��!Get the distance to your cinema: ��6    #��[ 6I   $��Center=�6v   $��	Underline{�6�   �� 5�      $
 
Horizontal=�6!   $��10q�6:   $��Center=�6F   .+�5�      �� $��Your address: ��6    #��[ 66   �� 5�      $��2q�6d   $	��150��6o   ��6"   �� *UserAddress+$ PropertyChanged=�6o   5�      �� 5�      $��Center=�6!   $
 
Horizontal=�6>   .+�5�      �� $��Get Distance��6!   $��10q�68   ��6D   �� *GetDistanceCommand+�� 5�      $��10q�6i   $	��100��6u    �� 
IsReadOnly . ��6"   �� *DistanceToCinema+$ PropertyChanged=�6�   5�      5�      5�      �� 5�      $ ��Check Out Film Information��6   .��5�      ��  �� CanVerticallyScroll . .+�5�      �� $��Movies:��6    $��Center=�6/   #��[ 6L   �� 5�      $	��300��6   $��5q�6+   ��5�      �� *Films_FilmInfo+ 5�      �� *SelectedFilm_FilmInfo+$ PropertyChanged=���5�      �� 5�      .R�5�      �� $
 
Horizontal=�6)   .+�5�   "   �� $��Uniform=�6(   $��5q�6:   $	��128��6E   $	��128��6Q   ��6^   �� *	image_url+# [  �� IsAsync . �� 5�   "   $��3q�6C   $��Center=�6N   $
��Bold"�6i   ��6,   �� *title+6i   5�      5�       5�        5�      $ SelectionChanged��6-   . 5�   "      68   �� *Film_Info_FilmChangedCommand+5�      5�      (  "System.Windows.Controls.WebBrowser  5�      $��20q�6!   $	��360��5�   !   "  UserWindow.WebBrowserUtility   BindableSource 5�   !   �� *CurrentReviewUrl+5�   !   �� 5�       .  �� TextWrapping$ WrapWithOverflow=�60   $��20q�6x   ��6P   �� *CurrentFilmDescription+6x   5�      5�      �� 5�      $��Check The Weather��6   .��5�      �� $	��200��6   .+�5�      ]� .+�5�      �� $��Weather Status:��6$   #��[ 6;   �� 5�      #��[ 6$   $��20(�6X   ��6f   �� *WeatherStatus+$ PropertyChanged=�5�      ]� 5�      .+�5�      �� $��Temperature:��6    #��[ 64   �� 5�      #��[ 6$   ��6X   �� *WeatherTemperature+$ PropertyChanged=�5�      ]� 5�      .+�5�      �� $��Wind:��6    #��[ 6-   �� 5�      #��[ 6$   ��6X   �� *WeatherWind+$ PropertyChanged=�5�      ]� 5�      .+�5�      �� $��Rain:��6    #��[ 6.   �� 5�      #��[ 6$   ��6X   �� *WeatherRain+$ PropertyChanged=�5�      �� 5�      $	��350��5�      $	��350��6)   $
��Fill=�65   ��5�      �� *WeatherImageSource+$ PropertyChanged=�# [  . 5�   5   5�      5�      5�      5�      5�      