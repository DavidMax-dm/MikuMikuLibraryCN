using MikuMikuLibrary.Geometry;
using MikuMikuLibrary.Numerics;
using MikuMikuLibrary.Objects;
using MikuMikuModel.GUI.Controls;
using MikuMikuModel.Nodes.Collections;
using Ookii.Dialogs.WinForms;
using System.IO;

using System.Text.Json;

namespace MikuMikuModel.Nodes.Objects;

public class MeshNode : Node<Mesh>
{
    public override NodeFlags Flags => NodeFlags.Add | NodeFlags.Rename;

    public override Control Control
    {
        get
        {
            var objectSetParent = FindParent<ObjectSetNode>();
            var objectParent = FindParent<ObjectNode>();

            if (objectSetParent == null || objectParent == null)
                return null;

            ModelViewControl.Instance.SetModel(Data, objectParent.Data, objectSetParent.Data.TextureSet);
            return ModelViewControl.Instance;
        }
    }

    [Category("General")]
    [DisplayName("Bounding sphere")]
    public BoundingSphere BoundingSphere
    {
        get => GetProperty<BoundingSphere>();
        set => SetProperty(value);
    }

    [Category("General")]
    public Vector3[] Positions
    {
        get => GetProperty<Vector3[]>();
        set => SetProperty(value);
    }

    [Category("General")]
    public Vector3[] Normals
    {
        get => GetProperty<Vector3[]>();
        set => SetProperty(value);
    }

    [Category("General")]
    public Vector4[] Tangents
    {
        get => GetProperty<Vector4[]>();
        set => SetProperty(value);
    }

    [Category("General")]
    [DisplayName("Texture coordinates 1")]
    public Vector2[] TexCoords0
    {
        get => GetProperty<Vector2[]>();
        set => SetProperty(value);
    }

    [Category("General")]
    [DisplayName("Texture coordinates 2")]
    public Vector2[] TexCoords1
    {
        get => GetProperty<Vector2[]>();
        set => SetProperty(value);
    }

    [Category("General")]
    [DisplayName("Texture coordinates 3")]
    public Vector2[] TexCoords2
    {
        get => GetProperty<Vector2[]>();
        set => SetProperty(value);
    }

    [Category("General")]
    [DisplayName("Texture coordinates 4")]
    public Vector2[] TexCoords3
    {
        get => GetProperty<Vector2[]>();
        set => SetProperty(value);
    }

    [Category("General")]
    [DisplayName("Colors 1")]
    public Vector4[] Colors0
    {
        get => GetProperty<Vector4[]>();
        set => SetProperty(value);
    }

    [Category("General")]
    [DisplayName("Colors 2")]
    public Vector4[] Colors1
    {
        get => GetProperty<Vector4[]>();
        set => SetProperty(value);
    }

    [Category("General")]
    [DisplayName("Blend weights")]
    public Vector4[] BlendWeights
    {
        get => GetProperty<Vector4[]>();
        set => SetProperty(value);
    }

    [Category("General")]
    [DisplayName("Blend indices")]
    public Vector4Int[] BlendIndices
    {
        get => GetProperty<Vector4Int[]>();
        set => SetProperty(value);
    }

    [Category("General")]
    [DisplayName("Flags")]
    public MeshFlags MeshFlags
    {
        get => GetProperty<MeshFlags>(nameof(Mesh.Flags));
        set => SetProperty(value, nameof(Mesh.Flags));
    }

    protected override void Initialize()
    {
        AddCustomHandler("Create color data", () =>
        {
            while (true)
            {
                using (var inputDialog = new InputDialog
                           { WindowTitle = "Please enter color values. (R, G, B, A)", Input = "1, 1, 1, 1" })
                {
                    if (inputDialog.ShowDialog() != DialogResult.OK)
                        break;

                    bool success = true;

                    var split = inputDialog.Input.Split(',').Select(x =>
                    {
                        success &= float.TryParse(x, out float value);
                        return value;
                    }).ToArray();

                    if (split.Length != 4 || !success)
                    {
                        MessageBox.Show("Please enter valid color values.", Program.Name, MessageBoxButtons.OK,
                            MessageBoxIcon.Error);

                        continue;
                    }

                    var color = new Vector4(split[0], split[1], split[2], split[3]);
                    var colors = new Vector4[Data.Positions.Length];
                    for (int i = 0; i < colors.Length; i++)
                        colors[i] = color;

                    Colors0 = colors;
                    break;
                }
            }
        });

        AddCustomHandler("Export Color Data", () =>
        {
            if (Colors0 == null || Colors0.Length == 0)
            {
                MessageBox.Show("No color data to export.", Program.Name, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var saveDialog = new SaveFileDialog
            {
                Title = "Export Color Data",
                Filter = "Text File (*.txt)|*.txt",
                FileName = "Colors0.txt"
            })
            {
                if (saveDialog.ShowDialog() != DialogResult.OK)
                    return;

                try
                {
                    var lines = Colors0.Select(c => $"{c.X}, {c.Y}, {c.Z}, {c.W}");
                    File.WriteAllLines(saveDialog.FileName, lines);
                    //MessageBox.Show("Export successful!", Program.Name, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Failed to export file:\n{ex.Message}", Program.Name, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        });

        AddCustomHandler("Import Color Data", () =>
        {
            using (var openDialog = new OpenFileDialog
            {
                Title = "Import Color Data",
                Filter = "Text File (*.txt)|*.txt"
            })
            {
                if (openDialog.ShowDialog() != DialogResult.OK)
                    return;

                try
                {
                    var lines = File.ReadAllLines(openDialog.FileName);
                    var importedColors = new List<Vector4>();

                    foreach (var line in lines)
                    {
                        var split = line.Split(',').Select(s => s.Trim()).ToArray();

                        if (split.Length != 4 ||
                            !float.TryParse(split[0], out float r) ||
                            !float.TryParse(split[1], out float g) ||
                            !float.TryParse(split[2], out float b) ||
                            !float.TryParse(split[3], out float a))
                        {
                            MessageBox.Show("Invalid data format in file.", Program.Name, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        importedColors.Add(new Vector4(r, g, b, a));
                    }

                    if (importedColors.Count != Data.Positions.Length)
                    {
                        MessageBox.Show($"Imported color count ({importedColors.Count}) doesn't match vertex count ({Data.Positions.Length}).",
                                        Program.Name, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    Colors0 = importedColors.ToArray();
                    //MessageBox.Show("Import successful!", Program.Name, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Failed to import file:\n{ex.Message}", Program.Name, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        });
    }

    protected override void PopulateCore()
    {
        Nodes.Add(new ListNode<SubMesh>("Submeshes", Data.SubMeshes));
    }

    protected override void SynchronizeCore()
    {
    }

    public MeshNode(string name, Mesh data) : base(name, data)
    {
    }
}
