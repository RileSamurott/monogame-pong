using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace MDTBCollisionSystem
{
    public abstract class Entity
    {
        public Vector2 position;
        protected BoundingBox2D boundingbox;
        public Texture2D sprite;
        protected bool immobile;

        public Vector2 tlCorner
        {
            get
            {
                return new Vector2(boundingbox.tlx, boundingbox.tly);
            }
        }
        public Vector2 brCorner
        {
            get
            {
                return new Vector2(boundingbox.brx, boundingbox.bry);
            }
        }
        public float X
        {
            get
            {
                return this.position.X;
            }
            set
            {
                this.position.X = value;
            }
        }
        public float Y
        {
            get
            {
                return this.position.Y;
            }
            set
            {
                this.position.Y = value;
            }
        }

        public virtual void collide(Entity other)
        {
            if (BoundingBox2D.checkOverlap(this.boundingbox, other.boundingbox)) {
                
            }
        }     
    }

    public class StaticEntity: Entity
    {
        public StaticEntity(string spriteName)
        {
            this.spriteName = spriteName;
            this.immobile = true;
        }
    }
    public class DynamicEntity: Entity
    {
        // Dynamic Entities have velocity and acceleration, unlike static entities which don't.
        protected float polarVelScalar = 0; // 
        protected float polarVelAngle = 0; // In radians
        /*
        protected static float accConstant = 1;
        protected float maxPolarSpeed = 100; 

        public void Update(GameTime gametime)
        {
            this.position.X += 
            this.velocity.X 
        }
        */
        public DynamicEntity()
        {
            this.immobile = false;
        }
    }
}
