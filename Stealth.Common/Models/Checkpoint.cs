using Rage;
using Rage.Native;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Stealth.Common.Models
{
    public class Checkpoint : IDeletable, ISpatial
    {
        private bool valid = false;
        private bool set_on_ground = true;

        private Vector3 _position;
        public Vector3 Position
        {
            get
            {
                return _position;
            }
            set
            {
                _position = value;
                ReCreateCheckpoint();
            }
        }

        private Vector3 _next_position;
        public Vector3 NextPosition
        {
            get
            {
                return _next_position;
            }
            set
            {
                _next_position = value;
                ReCreateCheckpoint();
            }
        }

        public float radius { get; private set; }
        public float height { get; private set; }
        private Color _color;
        public Color color
        {
            get
            {
                return _color;
            }
            set
            {
                UpdateColor(value);
            }
        }
        public int type { get; private set; }
        public int reserved { get; private set; }
        public int Handle { get; private set; }

        public Checkpoint(Vector3 Position, Color color, float radius, float height, int type = 47, int reserved = 0, bool set_on_ground = true)
        {
            this.type = type;
            this._position = Position;
            this.NextPosition = Position;
            this.radius = radius;
            this.color = color;
            this.reserved = reserved;
            this.set_on_ground = set_on_ground;
            this.height = height;

            CreateCheckpoint();
        }

        public Checkpoint(Vector3 Position, Vector3 NextPosition, Color color, float radius, float height, int type = 0, int reserved = 0, bool set_on_ground = true)
        {
            this.type = type;
            this._position = Position;
            this.NextPosition = NextPosition;
            this.radius = radius;
            this.color = color;
            this.reserved = reserved;
            this.set_on_ground = set_on_ground;
            this.height = height;

            CreateCheckpoint();
        }

        private void ReCreateCheckpoint()
        {
            this.Delete();
            CreateCheckpoint();
        }

        private void CreateCheckpoint()
        {
            Vector3 place_position = Position;
            if (set_on_ground)
            {
                float? groundZ = World.GetGroundZ(place_position, true, false);

                if (groundZ != null && groundZ.HasValue)
                {
                    place_position.Z = groundZ.Value;
                }
            }

            try
            {
                int _handle = NativeFunction.CallByName<int>("CREATE_CHECKPOINT", type, place_position.X, place_position.Y, place_position.Z, NextPosition.X, NextPosition.Y, NextPosition.Z, radius, color.R, color.G, color.B, color.A, reserved);
                Handle = _handle;
                valid = true;
                SetHeight(height, height, radius);
                Game.LogTrivialDebug("Created checkpoint, handle = " + _handle);
            }
            catch (Exception e)
            {
                Game.LogTrivialDebug("Exception trying to create checkpoint: " + e.Message);
                Game.LogTrivialDebug(e.StackTrace);
                valid = false;
            }
        }

        public void UpdateColor(Color new_color)
        {
            this._color = new_color;
            if (valid)
                NativeFunction.CallByName<uint>("SET_CHECKPOINT_RGBA", new_color.R, new_color.G, new_color.B, new_color.A);
        }

        public void SetHeight(float near, float far, float radius)
        {
            height = far;

            if (valid)
                NativeFunction.CallByName<uint>("SET_CHECKPOINT_CYLINDER_HEIGHT", Handle, near, far, radius);
        }

        public void Delete()
        {
            if (valid)
                NativeFunction.CallByName<uint>("DELETE_CHECKPOINT", Handle);

            valid = false;
        }

        public bool IsValid()
        {
            return valid;
        }

        public float DistanceTo(ISpatial target)
        {
            return Position.DistanceTo(target);
        }

        public float DistanceTo(Vector3 target)
        {
            return Position.DistanceTo(target);
        }

        public float DistanceTo2D(Vector3 target)
        {
            return Position.DistanceTo2D(target);
        }

        public float DistanceTo2D(ISpatial target)
        {
            return Position.DistanceTo2D(target);
        }

        public float TravelDistanceTo(Vector3 target)
        {
            return Position.TravelDistanceTo(target);
        }

        public float TravelDistanceTo(ISpatial target)
        {
            return Position.TravelDistanceTo(target);
        }

        public static implicit operator bool(Checkpoint checkpoint)
        {
            return checkpoint != null && checkpoint.IsValid();
        }
    }
}
