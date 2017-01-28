using System;
namespace UniBlocks.UnitTests
{
	class GLPoolResourceNode
	{
		public uint First { get; set; }
		public uint Last { get; set; }
		public uint Count { get; set; }
		public GLPoolResourceNode Next { get; set; }
	}

	class GLPoolResource<T> : IGLDescriptorPoolResource<T>
		where T : IGLDescriptorSetResource, new()
	{
		public GLPoolResource(uint noOfItems)
		{
			if (noOfItems == 0)
				throw new ArgumentOutOfRangeException(nameof(noOfItems) + " must be greater than zero");

			Items = new T[noOfItems];
			Head = new GLPoolResourceNode
			{
				First = 0,
				Last = noOfItems - 1,
				Count = noOfItems,
				Next = null,
			};
		}

		public void Reset()
		{
			throw new NotImplementedException();
		}

		public GLPoolResourceNode Head { get; private set; }

		public T[] Items
		{
			get;
			private set;
		}

		public bool Allocate(uint request, out GLPoolResourceInfo range)
		{
			if (request == 0)
			{
				throw new ArgumentOutOfRangeException(nameof(request) + " must be greater than 0"); 
			}

			{
				// FIRST LOOP : SCAN FOR EXACT MATCHES
				GLPoolResourceNode current = Head;
				GLPoolResourceNode previous = null;
				while (current != null)
				{
					if (current.Count == request)
					{
						range = new GLPoolResourceInfo
						{
							First = current.First,
							Last = current.Last,
							Count = current.Count,
						};

						// remove current from linked list
						if (previous != null)
						{
							previous.Next = current.Next;
						}

						// remove from head
						if (ReferenceEquals(Head, current))
						{
							Head = current.Next;
						}

						return true;
					}
					previous = current;
					current = current.Next;
				}
			}

			{
				// SECOND LOOP : FIND FIRST BLOCK LARGE ENOUGH AND SPLIT 
				GLPoolResourceNode current = Head;
				while (current != null)
				{
					if (current.Count > request)
					{
						range = new GLPoolResourceInfo
						{
							First = current.First,
							Last = request + current.First - 1,
							Count = request,
						};

						// adjust current
						current.First += request;
						current.Count -= request;
						return true;
					}
					current = current.Next;
				}
			}

			// NOT FOUND
			range = null;
			return false;
		}

	}
}
