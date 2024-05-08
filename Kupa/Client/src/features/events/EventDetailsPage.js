import { useEffect, useState  } from 'react';
import { useSelector, useDispatch } from 'react-redux';
import { useParams } from 'react-router-dom';
import { fetchEventById } from './eventsSlice';
import { postComment } from '../comments/commentsSlice';

const EventDetailsPage = () => {
  const { eventId } = useParams();
  const dispatch = useDispatch();
  const event = useSelector(state => state.events.currentEvent);
  const loading = useSelector(state => state.events.loading);
  const error = useSelector(state => state.events.error);

  const [commentText, setCommentText] = useState('');

  useEffect(() => {
    dispatch(fetchEventById(eventId));
  }, [dispatch, eventId]);

  const handleCommentSubmit = (e) => {
    e.preventDefault();
    if (commentText.trim()) {
      dispatch(postComment({ eventId, comment: commentText }));
      setCommentText('');
    }
  };

  if (loading) return <p>Loading...</p>;
  if (error) return <p>Error: {error}</p>;
  if (!event) return <p>No event found!</p>;

  return (
    <div className="p-4 max-w-4xl mx-auto">
      <h1 className="text-xl font-bold">{event.title}</h1>
      <p>{event.description}</p>
      <p><b>Status:</b> {event.status.name}</p>
      <p><b>Organisator:</b> {event.user?.profile?.name}</p>
      <p><b>Category:</b> {event.category.name}</p>
      <p><b>Created:</b> {event.createdAt}</p>
      <p><b>Description:</b> {event.description}</p>
      <p><b>Location:</b> {event.location.city.name} {event.location.address}</p>
      <br/>
      {event.eventComments && event.eventComments.length > 0 ? (
        <>
            <h1 className="text-xl font-bold">Comments</h1>

            <ul>
            {event.eventComments.map(comment => (
                <li key={comment.id} className="bg-gray-100 rounded p-3 my-2">
                <p className="text-sm text-gray-600">{comment.user.profile.name} on {new Date(comment.createdAt).toLocaleDateString()}</p>
                <p>{comment.comment}</p>
                </li>
            ))}
            </ul>
        </>
      ) : (
        <p>No comments yet.</p>
      )}

      <form onSubmit={handleCommentSubmit} className="mt-4">
        <textarea
          className="border rounded w-full p-2"
          placeholder="Write a comment..."
          value={commentText}
          onChange={(e) => setCommentText(e.target.value)}
        ></textarea>
        <button type="submit" className="mt-2 px-4 py-2 bg-blue-500 text-white rounded hover:bg-blue-700">
          Post Comment
        </button>
      </form>
    </div>
  );
};

export default EventDetailsPage;
