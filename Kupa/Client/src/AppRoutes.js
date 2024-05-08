import { Counter } from "./features/counter/Counter";
import EventsPage from "./features/events/EventsPage";
import EventDetailsPage from "./features/events/EventDetailsPage";
import { Home } from "./components/Home";

const AppRoutes = [
  {
    index: true,
    element: <Home />
  },
  {
    path: '/counter',
    element: <Counter />
  },
  {
    path: '/events',
    element: <EventsPage />
  },
  {
    path: '/events/:eventId',
    element: <EventDetailsPage />
  }
];

export default AppRoutes;
