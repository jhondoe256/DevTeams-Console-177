
using DevTeams.Data;
using DevTeams.Repository;
using static System.Console;

public class ProgramUI
{
    private readonly DevRepository _devRepo;
    private DevTeamRepository _devTeamRepo;
    private DevJobRepository _devJobRepo;
    public ProgramUI()
    {
        _devRepo = new DevRepository();
        _devTeamRepo = new DevTeamRepository(_devRepo);
        _devJobRepo = new DevJobRepository(_devRepo);
    }

    public void Run()
    {
        RunApplication();
    }

    private void RunApplication()
    {
        bool isRunning = true;
        while (isRunning)
        {
            Clear();
            WriteLine("Welcome to Komodo Dev Teams\n" +
                      "1.  Add Developer\n" +
                      "2.  List Developers\n" +
                      "3.  Get Dev By Id\n" +
                      "4.  Update Dev\n" +
                      "5.  Delete Dev\n" +
                      "============Teams=============\n" +
                      "6.  Add Dev Team\n" +
                      "7.  Dev Team Listing\n" +
                      "8.  Dev Team By Id\n" +
                      "9.  Update Dev Team\n" +
                      "10. Delete Dev Team\n" +
                      "============Challenge=============\n" +
                      "11. Get Devs without Ps\n" +
                      "0.  Close Application");

            var userInput = int.Parse(ReadLine()!);

            switch (userInput)
            {
                case 1:
                    AddDeveloper();
                    break;
                case 2:
                    ListDevelopers();
                    break;
                case 3:
                    GetDevById();
                    break;
                case 4:
                    UpdateDev();
                    break;
                case 5:
                    DeleteDev();
                    break;
                case 6:
                    AddDevTeam();
                    break;
                case 7:
                    DevTeamListing();
                    break;
                case 8:
                    DevTeamById();
                    break;
                case 9:
                    UpdateDevTeam();
                    break;
                case 10:
                    DeleteDevTeam();
                    break;
                case 11:
                    GetDevsWithoutPs();
                    break;
                case 0:
                    isRunning = CloseApplication();
                    break;
                default:
                    break;
            }
        }
    }

    private void DevTeamListing()
    {
        Clear();
        List<DevTeam> team = _devTeamRepo.GetDevTeams();
        if (team.Count() > 0)
        {
            DisplayDevTeamListing(team);
        }
        else
        {
            WriteLine("No teams available.");
        }

        PressAnyKey();
    }

    private void AddDevTeam()
    {
        Clear();

        DevTeam devTeamForm = UpsertDevTeam();

        //add team to the database
        if (_devTeamRepo.AddDevTeam(devTeamForm))
        {
            WriteLine("Success!");
        }
        else
        {
            WriteLine("Fail.");
        }

        PressAnyKey();
    }

    private DevTeam UpsertDevTeam()
    {
        DevTeam devTeamForm = new DevTeam();

        WriteLine("Please input Team name:");
        devTeamForm.Name = ReadLine()!;

        WriteLine("Do you want to add Developers to this team. (y/n)");

        string userSelection = ReadLine()!;
        if (userSelection == "Y".ToLower())
        {
            //Note: devsInDb is only a 'copy' of the repository data
            List<Developer> devsInDb = _devRepo.GetDevelopers();
            if (devsInDb.Count() > 0)
            {
                bool hasSelectedDevs = false;
                while (hasSelectedDevs == false)
                {
                    Clear();
                    DevListItems(devsInDb);

                    try
                    {
                        if (devsInDb.Count() > 0)
                        {
                            WriteLine("Please input a Developer Id.");
                            int userInput = int.Parse(ReadLine()!);

                            Developer dev = _devRepo.GetDeveloper(userInput);
                            if (dev is not null)
                            {
                                devTeamForm.DevsOnTeam.Add(dev);
                                //remove the member from 'devsInDb' (the copy)
                                devsInDb.Remove(dev);
                                WriteLine("Do you want to add another member. (y/n)");
                                string userInputAnotherMember = ReadLine()!;
                                if (userInputAnotherMember == "Y".ToLower())
                                {
                                    continue;
                                }
                                else
                                {
                                    hasSelectedDevs = true;
                                }
                            }
                        }
                        else
                        {
                            WriteLine("Sorry, No Developers are available.");
                            break;
                        }
                    }
                    catch (Exception ex)
                    {
                        WriteLine($"Sorry something went wrong: {ex.Message}");
                    }

                }
            }
            else
            {
                WriteLine("Sorry, No Developers are available.");
            }
        }

        return devTeamForm;
    }

    private void DevTeamById()
    {
        Clear();
        try
        {
            List<DevTeam> devTeamsInDb = _devTeamRepo.GetDevTeams();
            if (devTeamsInDb.Count() > 0)
            {
                DisplayDevTeamListing(devTeamsInDb);

                Write("Please select a Team by id: ");
                int userInput = int.Parse(ReadLine()!);

                DevTeam devTeam = _devTeamRepo.GetDevTeam(userInput);
                if (devTeam is not null)
                {
                    WriteLine(devTeam);
                }
                else
                {
                    WriteLine($"Team with the id: {userInput} doesn't exist.");
                }
            }
            else
            {
                WriteLine($"Sorry, No DevTeams are available.");
            }
        }
        catch (Exception ex)
        {
            WriteLine($"Sorry, something went wrong: {ex.Message}.");
        }

        PressAnyKey();
    }

    private void DisplayDevTeamListing(List<DevTeam> devTeamsInDb)
    {
        foreach (var team in devTeamsInDb)
        {
            WriteLine($"{team.Id} - {team.Name}\n");
        }
    }

    private void UpdateDevTeam()
    {
        Clear();
        try
        {
            if (_devTeamRepo.GetDevTeams().Count() > 0)
            {
                DisplayDevTeamListing(_devTeamRepo.GetDevTeams());

                WriteLine("Please enter a Team Id.");
                int userInput = int.Parse(ReadLine()!);
                DevTeam team = _devTeamRepo.GetDevTeam(userInput);
                if (team is not null)
                {
                    Clear();
                    DevTeam newDevTeamData = UpsertDevTeam();
                    if (_devTeamRepo.UpdateDevTeam(team.Id, newDevTeamData))
                    {
                        WriteLine("Success");
                    }
                    else
                    {
                        WriteLine("Fail.");
                    }
                }
                else
                {
                    WriteLine($"Invalid Id entry: {userInput}");
                }
            }
            else
            {
                WriteLine("Sorry No Teams Available.");
            }
        }
        catch (Exception ex)
        {
            WriteLine($"Something went wrong: {ex.Message}.");
        }

        PressAnyKey();
    }

    private void DeleteDevTeam()
    {
        Clear();
        try
        {
            if (_devTeamRepo.GetDevTeams().Count() > 0)
            {
                DisplayDevTeamListing(_devTeamRepo.GetDevTeams());

                WriteLine("Please enter a Team Id.");
                int userInput = int.Parse(ReadLine()!);
                DevTeam team = _devTeamRepo.GetDevTeam(userInput);
                if (team is not null)
                {
                    if (_devTeamRepo.DeleteDevTeam(team.Id))
                    {
                        WriteLine("Success!");
                    }
                    else
                    {
                        WriteLine("Fail!");
                    }
                }
                else
                {
                    WriteLine($"Invalid Id entry: {userInput}");
                }
            }
            else
            {
                WriteLine("Sorry No Teams Available.");
            }
        }
        catch (Exception ex)
        {
            WriteLine($"Something went wrong: {ex.Message}.");
        }

        PressAnyKey();
    }

    private void ListDevelopers()
    {
        Clear();
        List<Developer> devsInDb = _devRepo.GetDevelopers();
        foreach (Developer dev in devsInDb)
        {
            WriteLine(dev);

        }
        PressAnyKey();
    }

    private void AddDeveloper()
    {
        Clear();
        Developer dev = UpsertDev();
        // string message =_devRepo.AddDeveloper(dev) ? "Success" : "Fail";
        // WriteLine(message);

        if (_devRepo.AddDeveloper(dev))
        {
            WriteLine("Success!");
        }
        else
        {
            WriteLine("Fail!");
        }

        PressAnyKey();
    }

    private Developer UpsertDev()
    {
        WriteLine("What kind of Developer are you?\n" +
                  "1. Front End\n" +
                  "2. Back End\n" +
                  "3. Full Stack\n");

        int userInputDevType = int.Parse(ReadLine()!);


        if (userInputDevType == 1)
        {
            var developerF = DevForm(new FrontEndDeveloper());
            return developerF;
        }
        if(userInputDevType == 2)
        {
            var developerB = DevForm(new BackendDeveloper());
            return developerB;
        }
        if (userInputDevType == 3)
        {
            var developerFullStack = DevForm(new FullStackDeveloper());
            return developerFullStack;
        }
        else
        {
            return null;
        }

    }


    private Developer DevForm(Developer developer)
    {
        Write("Please input a First Name: ");
        string userInput_FirstName = ReadLine()!;
        developer.FirstName = userInput_FirstName;

        Write("Please input a Last Name: ");
        developer.LastName = ReadLine()!;

        WriteLine("Does this Developer have a Pluralsight accunt. y/n");

        string hasPs = ReadLine()!;

        if (hasPs == "Y".ToLower())
            developer.HasPluralsight = true;
        else
            developer.HasPluralsight = false;

        return developer;
    }


    private void GetDevById()
    {
        Clear();
        List<Developer> devsInDb = _devRepo.GetDevelopers();
        if (devsInDb.Count() > 0)
        {
            try
            {
                DevListItems(devsInDb);

                Write("Please select a DevId: ");
                int devId = int.Parse(ReadLine()!);

                Developer selectedDev = _devRepo.GetDeveloper(devId);
                if (selectedDev is not null)
                {
                    Clear();
                    WriteLine(selectedDev);
                }
                else
                {
                    WriteLine("Sorry, invalid Id.");
                }

            }
            catch (Exception ex)
            {
                WriteLine($"Somthing went wrong: {ex.Message}");
            }
        }
        else
        {
            WriteLine("Sorry no available Developers.");
        }

        PressAnyKey();
    }

    private void DevListItems(List<Developer> developers)
    {
        foreach (var dev in developers)
        {
            WriteLine($"{dev.Id} : {dev.FullName}\n");
        }
    }
    private void UpdateDev()
    {
        Clear();
        List<Developer> devsInDb = _devRepo.GetDevelopers();
        if (devsInDb.Count() > 0)
        {
            try
            {
                DevListItems(devsInDb);

                Write("Please select a DevId: ");
                int devId = int.Parse(ReadLine()!);

                Developer selectedDev = _devRepo.GetDeveloper(devId);
                if (selectedDev is not null)
                {
                    Clear();
                    Developer newDevData = UpsertDev();
                    string message = _devRepo.UpdateDeveloper(devId, newDevData) ? "Success" : "Fail";
                    WriteLine(message);
                }
                else
                {
                    WriteLine("Sorry, invalid Id.");
                }

            }
            catch (Exception ex)
            {
                WriteLine($"Somthing went wrong: {ex.Message}");
            }
        }
        else
        {
            WriteLine("Sorry no available Developers.");
        }

        PressAnyKey();
    }

    private void DeleteDev()
    {
        Clear();
        List<Developer> devsInDb = _devRepo.GetDevelopers();
        if (devsInDb.Count() > 0)
        {
            try
            {
                DevListItems(devsInDb);

                Write("Please select a DevId: ");
                int devId = int.Parse(ReadLine()!);

                Developer selectedDev = _devRepo.GetDeveloper(devId);
                if (selectedDev is not null)
                {
                    if (_devRepo.DeleteDeveloper(selectedDev))
                    {
                        WriteLine("Success.");
                    }
                    else
                    {
                        WriteLine("Fail.");
                    }
                }
            }
            catch (Exception ex)
            {
                WriteLine($"Somthing went wrong: {ex.Message}");
            }
        }
        else
        {
            WriteLine("Sorry no available Developers.");
        }

        PressAnyKey();
    }

    private void GetDevsWithoutPs()
    {
        Clear();
        WriteLine("Devs without Pluralsight:");
        List<Developer> devs = _devRepo.GetDevelopersWithoutPs();
        devs.ForEach(d => WriteLine(d));

        // foreach (Developer developer in devs)
        // {
        //     WriteLine(developer);
        // }

        PressAnyKey();
    }

    private bool CloseApplication()
    {
        Clear();
        WriteLine("Thx");
        return false;
    }

    private void PressAnyKey()
    {
        WriteLine("Press any key to continue.");
        ReadKey();
    }
}
