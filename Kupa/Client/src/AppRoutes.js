import Home from "./components/Home";
import EventDetailsPage from "./features/events/EventDetailsPage";
import Login from "./features/auth/Login";

const AppRoutes = [
  {
    index: true,
    element: <Home />
  },
  {
    path: '/login',
    element: <Login />
  },
  {
    path: '/events/:eventId',
    element: <EventDetailsPage />
  }
];

export default AppRoutes;
