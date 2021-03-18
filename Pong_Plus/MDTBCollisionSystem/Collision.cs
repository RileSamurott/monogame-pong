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
    /// A Spatial Hash implementation which is used for camera checks as well as internal collision checks.
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

        public void addDynamicEntity(Entity entity)
        {
            foreach(int hash in getHash(entity.)
        }







        
    }
}
