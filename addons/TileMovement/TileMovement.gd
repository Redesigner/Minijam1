tool
extends EditorPlugin


func _enter_tree():
	# Initialization of the plugin goes here.
	# Add the new type with a name, a parent type, a script and an icon.
	add_custom_type("TileActor", 	"Node2D", preload("TileActor.cs"), 	preload("taIcon.png"))
	add_custom_type("TileGrid", 	"Node2D", preload("TileGrid.cs"), 	preload("taIcon.png"))


func _exit_tree():
	# Clean-up of the plugin goes here.
	# Always remember to remove it from the engine when deactivated.
	remove_custom_type("TileActor")
	remove_custom_type("TileGrid")
