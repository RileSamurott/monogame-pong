using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Xna.Framework;



namespace MDTBCollisionSystem
{
    public struct BoundingBox2D
    {
        public float tlx;
        public float tly;
        public float brx;
        public float bry;

        public BoundingBox2D(float tlx, float tly, float brx, float bry)
        {
            this.tlx = tlx;
            this.tly = tly;
            this.brx = brx;
            this.bry = bry;
        }

        public float Height
        {
            get { return Math.Abs(bry - tly); }
            set { bry = tly + value; }
        }

        public float Width
        {
            get { return Math.Abs(brx - tlx); }
            set { brx = tlx + value; }
        }
        public float X
        {
            get { return tlx; }
            set
            {
                tlx += value;
                brx += value;
            }
        }
        public float Y
        {
            get { return tly; }
            set
            {
                tly += value;
                bry += value;
            }
        }
        // Checks for overlaps between two bounding boxes.
        public static bool checkOverlap(BoundingBox2D a, BoundingBox2D b)
        {
            if (
                a.tlx < b.brx &&
                a.brx > b.tlx &&
                a.tly < b.bry &&
                a.bry > b.tly
                )
            {
                return true;
            }
            return false;
        }
    }

    /// <summary>
    /// A Spatial Hash implementation which is used for internal collision checks.
    /// Also might be used for camera? idek lol
    /// </summary>
    public class SpatialHash
    {
        private Hashtable contents;
        private int height;
        private int width;
        private int vtcells;
        private int hzcells;
        public List<Entity> dynamicEntities;
        public List<Entity> staticEntities;
        private SpatialHash staticState;

        private bool unsaved = true;

        public SpatialHash(int height, int width, int vertical, int horizontal)
        {
            this.height = height;
            this.width = width;
            this.vtcells = vertical;
            this.hzcells = horizontal;
        }

        private int getHash(Vector2 input)
        {
            return (int)(input.Y * hzcells + input.X);
        }
        private int getHash(int x, int y)
        {
            return x * hzcells + y;
        }

        public List<int> cellKeys(Vector2 mincoords, Vector2 maxcoords)
        {
            List<int> found = new List<int>();
            //Scale vectors
            int x1 = (int)Math.Floor(mincoords.X / hzcells);
            int x2 = (int)Math.Ceiling(maxcoords.X / hzcells);
            int y1 = (int)Math.Floor(mincoords.Y / vtcells);
            int y2 = (int)Math.Ceiling(maxcoords.Y / vtcells);
            for (int y = y1; y <= y2; y++)
            {
                for (int x = x1; x <= x2; x++)
                {
                    found.Add(getHash(x, y));
                }
            }
            return found;
        }

        /// <summary>
        /// Saves all current static entities and saves them to a default object.
        /// Used for quicker reloading to prevent you having to re-add all the static entities one by one during collision updates.
        /// Only saves static entities like tiles that don't move; doesn't save dynamic entities
        /// </summary>
        public void saveStaticDefault()
        {
            SpatialHash temp = new SpatialHash(this.height, this.width, this.vtcells, this.hzcells);
            foreach (StaticEntity ent in this.staticEntities) // A bit cumbersome, but seems okay for now
            {
                temp.addEntity(ent);
            }
                this.staticState = temp;
            this.unsaved = false;
        }

        public int addEntity(DynamicEntity entity)
        {
            List<Entity> cellData;
            foreach (int hash in cellKeys(entity.tlCorner,entity.brCorner))
            {
                if (!contents.ContainsKey(hash))
                {
                    cellData = new List<Entity>();
                }
                else
                {
                    cellData = (List<Entity>)contents[hash];
                }
                cellData.Add(entity);
                dynamicEntities.Add(entity);
                contents.Add(hash, cellData);
            }
            return dynamicEntities.Count - 1;
        }
        public int addEntity(StaticEntity entity)
        {
            List<Entity> cellData;
            foreach (int hash in cellKeys(entity.tlCorner, entity.brCorner))
            {
                if (!contents.ContainsKey(hash))
                {
                    cellData = new List<Entity>();
                }
                else
                {
                    cellData = (List<Entity>)contents[hash];
                }
                cellData.Add(entity);
                staticEntities.Add(entity);
                contents.Add(hash, cellData);
            }
            this.unsaved = true;
            return staticEntities.Count - 1;
        }

        /// <summary>
        /// Resets the board, clearing it of all dynamic objects.
        /// This is largely in preparation for readding dynamic objects in collision checks.
        /// </summary>
        public void clear()
        {
            this.contents = this.staticState.contents;
            this.dynamicEntities = new List<Entity>();
            if (this.unsaved)
            {
                this.saveStaticDefault();
            }
        }

        public List<Entity> overlapsFor(int entityIndex)
        {

        }
        








    }
}
