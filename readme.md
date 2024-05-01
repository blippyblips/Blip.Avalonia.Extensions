# Installation

Add under <Application.Styles>:
    <StyleInclude Source="avares://Ex/ExCaptionButtons.axaml" />
    <StyleInclude Source="avares://Ex/ExTitleBar.axaml" />

Add xmlns:ex="https://github.com/blippyblips/ex" to your control/window

# Sample code from a main window


<ex:ExTitleBar DockPanel.Dock="Top">
  <ex:ExTitleBar.InnerLeftContent>
    <StackPanel Orientation="Horizontal" Margin="5,0,0,0">
      <Image Source="avares://ExCom/Assets/main.ico" Width="16" Height="16" />
      <Menu Margin="5,0,0,0">
        <MenuItem Header="{x:Static assets:Resources.MenuFile}" IsTabStop="False">
          <MenuItem Command="{Binding Exit}" Header="{x:Static assets:Resources.MenuExit}" IsTabStop="False" />
        </MenuItem>
        <MenuItem Header="{x:Static assets:Resources.MenuView}" IsTabStop="False">
          <MenuItem Command="{Binding Sort}" CommandParameter="{x:Static vm:SortingBy.SortingByName}" Header="{x:Static assets:Resources.MenuSortByName}" />
          <MenuItem Command="{Binding Sort}" CommandParameter="{x:Static vm:SortingBy.SortingByExt}" Header="{x:Static assets:Resources.MenuSortByExtension}" />
          <MenuItem Command="{Binding Sort}" CommandParameter="{x:Static vm:SortingBy.SortingBySize}" Header="{x:Static assets:Resources.MenuSortBySize}" />
          <MenuItem Command="{Binding Sort}" CommandParameter="{x:Static vm:SortingBy.SortingByDate}" Header="{x:Static assets:Resources.MenuSortByDate}" />
        </MenuItem>
        <MenuItem Header="{x:Static assets:Resources.MenuEdit}" IsTabStop="False">
          <MenuItem Command="{Binding Options}" Header="{x:Static assets:Resources.MenuOptions}" />
        </MenuItem>
        <MenuItem Header="{x:Static assets:Resources.MenuConfiguration}" IsTabStop="False">
          <MenuItem Command="{Binding Options}" Header="{x:Static assets:Resources.MenuOptions}" />
        </MenuItem>
      </Menu>
    </StackPanel>
  </ex:ExTitleBar.InnerLeftContent>
</ex:ExTitleBar>

# Another sample from a dialog

  <Border>
    <DockPanel>
      <ex:ExTitleBar DockPanel.Dock="Top" IsDialog="True">
        <ex:ExTitleBar.InnerLeftContent>
          <Menu>
            <MenuItem Header="{x:Static assets:Resources.MenuFile}" IsTabStop="False">
              <MenuItem Command="" Header="Open" />
              <MenuItem Command="" Header="{x:Static assets:Resources.MenuExit}" IsTabStop="False" />
            </MenuItem>
            <MenuItem Header="{x:Static assets:Resources.MenuView}" IsTabStop="False">
              <MenuItem Command="" Header="Copy" />
              <MenuItem Command="" Header="Copy as plaintext" />
            </MenuItem>
          </Menu>
        </ex:ExTitleBar.InnerLeftContent>
      </ex:ExTitleBar>
      <Grid ColumnDefinitions="*" RowDefinitions="Auto, *">

        <ScrollViewer Grid.Row="1">
          <TextBox Text="{Binding FileContents}" Margin="10" FontSize="18" IsReadOnly="True" TextWrapping="Wrap" />
        </ScrollViewer>
      </Grid>
    </DockPanel>
  </Border>


# Notes
The IsDialog is forwarded to the CaptionButtons which tells the Close Application (X) button to act as a cancel button. By default that means that Escape is keybound as close this window.